﻿// <auto-generated />
using System;
using JetStreamData.FlightsService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JetStreamData.FlightsService.Infrastructure.Migrations
{
    [DbContext(typeof(FlightDbContext))]
    [Migration("20240409024244_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("JetStreamData.FlightsService.Domain.Entities.FlightInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.HasKey("Id");

                    b.ToTable("FlightInformation", (string)null);
                });

            modelBuilder.Entity("JetStreamData.FlightsService.Domain.Entities.FlightInformation", b =>
                {
                    b.OwnsOne("JetStreamData.FlightsService.Domain.Entities.Airline", "Airline", b1 =>
                        {
                            b1.Property<Guid>("FlightInformationId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("Iata")
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)");

                            b1.Property<string>("Icao")
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)");

                            b1.Property<DateTime>("ModifiedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("Name")
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)");

                            b1.HasKey("FlightInformationId");

                            b1.ToTable("FlightInformation");

                            b1.WithOwner()
                                .HasForeignKey("FlightInformationId");
                        });

                    b.OwnsOne("JetStreamData.FlightsService.Domain.Entities.Flight", "Flight", b1 =>
                        {
                            b1.Property<Guid>("FlightInformationId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("ModifiedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<int?>("Status")
                                .HasColumnType("int");

                            b1.HasKey("FlightInformationId");

                            b1.ToTable("FlightInformation");

                            b1.WithOwner()
                                .HasForeignKey("FlightInformationId");
                        });

                    b.OwnsOne("JetStreamData.FlightsService.Domain.Entities.Airport", "ArrivalAirport", b1 =>
                        {
                            b1.Property<Guid>("FlightInformationId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("IataCode")
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)");

                            b1.Property<string>("IcaoCode")
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)");

                            b1.Property<DateTime>("ModifiedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("Name")
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)");

                            b1.Property<string>("Timezone")
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)");

                            b1.HasKey("FlightInformationId");

                            b1.ToTable("FlightInformation");

                            b1.WithOwner()
                                .HasForeignKey("FlightInformationId");
                        });

                    b.OwnsOne("JetStreamData.FlightsService.Domain.Entities.Schedule", "ArrivalSchedule", b1 =>
                        {
                            b1.Property<Guid>("FlightInformationId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime?>("Actual")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime?>("Estimated")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("ModifiedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("Scheduled")
                                .HasColumnType("datetime(6)");

                            b1.HasKey("FlightInformationId");

                            b1.ToTable("FlightInformation");

                            b1.WithOwner()
                                .HasForeignKey("FlightInformationId");
                        });

                    b.OwnsOne("JetStreamData.FlightsService.Domain.Entities.Airport", "DepartureAirport", b1 =>
                        {
                            b1.Property<Guid>("FlightInformationId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("IataCode")
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)");

                            b1.Property<string>("IcaoCode")
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)");

                            b1.Property<DateTime>("ModifiedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("Name")
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)");

                            b1.Property<string>("Timezone")
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)");

                            b1.HasKey("FlightInformationId");

                            b1.ToTable("FlightInformation");

                            b1.WithOwner()
                                .HasForeignKey("FlightInformationId");
                        });

                    b.OwnsOne("JetStreamData.FlightsService.Domain.Entities.Schedule", "DepartureSchedule", b1 =>
                        {
                            b1.Property<Guid>("FlightInformationId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime?>("Actual")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime?>("Estimated")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("ModifiedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("Scheduled")
                                .HasColumnType("datetime(6)");

                            b1.HasKey("FlightInformationId");

                            b1.ToTable("FlightInformation");

                            b1.WithOwner()
                                .HasForeignKey("FlightInformationId");
                        });

                    b.Navigation("Airline");

                    b.Navigation("ArrivalAirport");

                    b.Navigation("ArrivalSchedule");

                    b.Navigation("DepartureAirport");

                    b.Navigation("DepartureSchedule");

                    b.Navigation("Flight");
                });
#pragma warning restore 612, 618
        }
    }
}
