using LetMePark.Core.Entities;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Core.Policies;

internal sealed class BossReservationPolicy : IReservationPolicy
{
    public bool CanBeApplied(JobTitle jobTitle)
        => jobTitle == JobTitle.Boss;

    public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
        => true;
}