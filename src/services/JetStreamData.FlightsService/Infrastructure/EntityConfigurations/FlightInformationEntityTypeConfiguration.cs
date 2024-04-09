using JetStreamData.FlightsService.Domain.Entities;
using JetStreamData.FlightsService.Domain.Enums;
using JetStreamData.Kernel.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JetStreamData.FlightsService.Infrastructure.EntityConfigurations;

public class FlightInformationEntityTypeConfiguration : AggregateRootTypeConfiguration<FlightInformation, Guid>
{
    public override void Configure(EntityTypeBuilder<FlightInformation> builder)
    {
        base.Configure(builder);

        builder.ToTable("FlightInformation");

        builder.OwnsOne(fi => fi.Flight, flight =>
        {
            flight.WithOwner();
            flight.Ignore(f => f.Id);
            flight.Property(f => f.Number).IsRequired();
            flight.Property(f => f.Status)
                .HasConversion(new ValueConverter<FlightStatus, int>(
                    v => v.Id,
                    v => FlightStatus.Parse(v)));
        });

        builder.OwnsOne(fi => fi.DepartureAirport, airport =>
        {
            airport.WithOwner();
            airport.Ignore(a => a.Id);
            airport.Property(a => a.Name).HasMaxLength(256);
            airport.Property(a => a.Timezone).HasMaxLength(256);
            airport.Property(a => a.IataCode).HasMaxLength(256);
            airport.Property(a => a.IcaoCode).HasMaxLength(256);
        });

        builder.OwnsOne(fi => fi.ArrivalAirport, airport =>
        {
            airport.WithOwner();
            airport.Ignore(a => a.Id);
            airport.Property(a => a.Name).HasMaxLength(256);
            airport.Property(a => a.Timezone).HasMaxLength(256);
            airport.Property(a => a.IataCode).HasMaxLength(256);
            airport.Property(a => a.IcaoCode).HasMaxLength(256);
        });

        builder.OwnsOne(fi => fi.Airline, airline =>
        {
            airline.WithOwner();
            airline.Ignore(a => a.Id);
            airline.Property(a => a.Name).HasMaxLength(256);
            airline.Property(a => a.Iata).HasMaxLength(256);
            airline.Property(a => a.Icao).HasMaxLength(256);
        });

        builder.OwnsOne(fi => fi.DepartureSchedule, schedule =>
        {
            schedule.WithOwner();
            schedule.Ignore(s => s.Id);
        });

        builder.OwnsOne(fi => fi.ArrivalSchedule, schedule =>
        {
            schedule.WithOwner();
            schedule.Ignore(s => s.Id);
        });

        builder
            .Property(fi => fi.RowVersion)
            .IsRowVersion();
    }
}
