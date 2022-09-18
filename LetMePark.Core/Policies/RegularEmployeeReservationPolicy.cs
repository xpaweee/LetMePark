﻿using LetMePark.Core.Abstractions;
using LetMePark.Core.Entities;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Core.Policies;

internal sealed class RegularEmployeeReservationPolicy : IReservationPolicy
{
    private readonly IClock _clock;

    public RegularEmployeeReservationPolicy(IClock clock)
    {
        _clock = clock;
    }

    public bool CanBeApplied(JobTitle jobTitle)
        => jobTitle.Value == JobTitle.Employee;

    public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
    {
        var totalEmployeeReservations = weeklyParkingSpots.SelectMany(x => x.Reservations).OfType<VehicleReservation>().Count(x => x.EmployeeName == employeeName);

        return totalEmployeeReservations < 2 && _clock.Current().Hour > 4;
    }
}