using LetMePark.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetMePark.Infrastructure.DAL.Configurations;

internal sealed  class CleaningReservationConfiguration : IEntityTypeConfiguration<CleaningReservation>
{
    public void Configure(EntityTypeBuilder<CleaningReservation> builder)
    {
    }
}