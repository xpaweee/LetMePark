using LetMePark.Core.Entities;
using LetMePark.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetMePark.Infrastructure.DAL.Configurations;

internal sealed class VehicleReservationConfiguration :  IEntityTypeConfiguration<VehicleReservation>
{
    public void Configure(EntityTypeBuilder<VehicleReservation> builder)
    {
        builder.Property(x => x.EmployeeName)
            .HasConversion(x => x.Value, x => new EmployeeName(x));  
        builder.Property(x => x.LicensePlate)
            .HasConversion(x => x.Value, x => new LicensePlate(x));  
    }
}