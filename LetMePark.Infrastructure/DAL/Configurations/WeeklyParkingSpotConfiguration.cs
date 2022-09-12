using LetMePark.Core.Entities;
using LetMePark.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMePark.Infrastructure.DAL.Configurations
{
    internal sealed class WeeklyParkingSpotConfiguration : IEntityTypeConfiguration<WeeklyParkingSpot>
    {
        public void Configure(EntityTypeBuilder<WeeklyParkingSpot> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new ParkingSpotId(x));
            builder.Property(x => x.Week)
                .HasConversion(x => x.To.Value, x => new Week(x));
        }
    }
}
