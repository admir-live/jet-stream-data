using System.Data;
using JetStreamData.Kernel.Dispatcher;
using JetStreamData.Kernel.Domain.Entities;
using JetStreamData.Kernel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace JetStreamData.Kernel.Infrastructure.Data;

public abstract class JetStreamDataBaseDbContext(
    DbContextOptions dbContextOptions,
    IDispatcher dispatcher) : DbContext(dbContextOptions), IUnitOfWork
{
    private bool IsDisposed { get; set; }
    public IDbContextTransaction CurrentTransaction { get; private set; }

    public bool HasActiveTransaction => CurrentTransaction != null;
    public abstract string Schema { get; }

    public override int SaveChanges()
    {
        DispatchPendingDomainEvents().GetAwaiter().GetResult();
        ApplyPostProcessingChanges();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        DispatchPendingDomainEvents().GetAwaiter().GetResult();
        ApplyPostProcessingChanges();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        await DispatchPendingDomainEvents();
        ApplyPostProcessingChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        await DispatchPendingDomainEvents();
        ApplyPostProcessingChanges();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override void Dispose()
    {
        if (IsDisposed)
        {
            return;
        }

        IsDisposed = true;
        CurrentTransaction?.Dispose();
        base.Dispose();
    }

    protected virtual void CheckDisposedStatus()
    {
        switch (IsDisposed)
        {
            case true:
                throw new ObjectDisposedException(GetType().FullName);
        }
    }

    private async Task DispatchPendingDomainEvents()
    {
        var entitiesWithDomainEvents = ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(entity => entity.Entity.DomainEvents != null && entity.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = entitiesWithDomainEvents
            .SelectMany(entity => entity.Entity.DomainEvents)
            .ToList();

        entitiesWithDomainEvents.ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await dispatcher.DispatchAsync(domainEvent);
        }
    }

    public async Task<IDbContextTransaction> StartNewTransactionAsync()
    {
        return CurrentTransaction ??= await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
    }

    public async Task CommitCurrentTransactionAsync(IDbContextTransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        if (transaction != CurrentTransaction)
        {
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not the active transaction");
        }

        try
        {
            await SaveChangesAsync();
            await CurrentTransaction?.CommitAsync()!;
        }
        catch
        {
            CancelTransaction();
            throw;
        }
        finally
        {
            CurrentTransaction?.Dispose();
            CurrentTransaction = null;
        }
    }

    public void CancelTransaction()
    {
        try
        {
            CurrentTransaction?.Rollback();
        }
        catch (InvalidOperationException)
        {
            // TODO: Handle exception for non-completed transaction
        }
        finally
        {
            CurrentTransaction?.Dispose();
            CurrentTransaction = null;
        }
    }

    private void ApplyPostProcessingChanges()
    {
        UpdateEntityModificationTimestamp();
    }

    private void UpdateEntityModificationTimestamp()
    {
        var modifiedEntities = ChangeTracker
            .Entries()
            .Where(entry => entry is { Entity: BaseEntity, State: EntityState.Added or EntityState.Modified })
            .ToList();

        foreach (var entry in modifiedEntities)
        {
            if (entry.Entity is BaseEntity trackingEntity)
            {
                trackingEntity.ModifiedAt = DateTime.UtcNow;
            }
        }
    }
}
