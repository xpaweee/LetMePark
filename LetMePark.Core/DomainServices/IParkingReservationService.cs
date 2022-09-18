﻿using LetMePark.Core.Entities;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Core.DomainServices;

public interface IParkingReservationService
{
    void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpots, JobTitle jobTitle, WeeklyParkingSpot parkingSpotToReserve, VehicleReservation reservation);

    void ReserveParkingForCleaning(IEnumerable<WeeklyParkingSpot> allParkingSpots, Date date);
}