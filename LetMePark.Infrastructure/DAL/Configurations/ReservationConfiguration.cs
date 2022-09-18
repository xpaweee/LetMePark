﻿using LetMePark.Core.Entities;
using LetMePark.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetMePark.Infrastructure.DAL.Configurations
{
    internal sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new ReservationId(x)); 
            builder.Property(x => x.ParkingSpotId)
                .HasConversion(x => x.Value, x => new ParkingSpotId(x));  
            builder.Property(x => x.Date)
                .HasConversion(x => x.Value, x => new Date(x));


            builder.HasDiscriminator<string>("Type")
                .HasValue<CleaningReservation>(nameof(CleaningReservation))
                .HasValue<VehicleReservation>(nameof(VehicleReservation));
        }
    }
}
