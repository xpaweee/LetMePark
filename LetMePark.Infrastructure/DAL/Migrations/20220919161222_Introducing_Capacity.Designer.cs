﻿// <auto-generated />
using System;
using LetMePark.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LetMePark.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(LetMeParkDbContext))]
    [Migration("20220919161222_Introducing_Capacity")]
    partial class Introducing_Capacity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LetMePark.Core.Entities.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ParkingSpotId")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("WeeklyParkingSpotId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WeeklyParkingSpotId");

                    b.ToTable("Reservations");

                    b.HasDiscriminator<string>("Type").HasValue("Reservation");
                });

            modelBuilder.Entity("LetMePark.Core.Entities.WeeklyParkingSpot", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("Week")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("WeeklyParkingSpots");
                });

            modelBuilder.Entity("LetMePark.Core.Entities.CleaningReservation", b =>
                {
                    b.HasBaseType("LetMePark.Core.Entities.Reservation");

                    b.HasDiscriminator().HasValue("CleaningReservation");
                });

            modelBuilder.Entity("LetMePark.Core.Entities.VehicleReservation", b =>
                {
                    b.HasBaseType("LetMePark.Core.Entities.Reservation");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("text");

                    b.Property<string>("LicensePlate")
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("VehicleReservation");
                });

            modelBuilder.Entity("LetMePark.Core.Entities.Reservation", b =>
                {
                    b.HasOne("LetMePark.Core.Entities.WeeklyParkingSpot", null)
                        .WithMany("Reservations")
                        .HasForeignKey("WeeklyParkingSpotId");
                });

            modelBuilder.Entity("LetMePark.Core.Entities.WeeklyParkingSpot", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
