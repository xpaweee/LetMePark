﻿using LetMePark.Core.Exceptions;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Core.Entities;

public class WeeklyParkingSpot
{
    public const int MaxCapacity = 2;
    public ParkingSpotId Id { get; private set; }
    public Week Week { get; private set; }
    public string Name { get; private set; }

    public Capacity Capacity { get; private set; }

    private readonly HashSet<Reservation> _reservations = new();
    public HashSet<Reservation> Reservations => _reservations;


    private WeeklyParkingSpot(ParkingSpotId id, Week week, string name, Capacity capacity)
    {
        Id = id;
        Week = week;
        Name = name;
        Capacity = capacity;
    }

    public static WeeklyParkingSpot Create(ParkingSpotId id, Week week, string name)
        => new(id, week, name, MaxCapacity);

    internal void AddReservation(Reservation reservation, Date now)
    {
        var isInvalidDate = reservation.Date < Week.From || reservation.Date > Week.To ||
                            reservation.Date < now;
        if (isInvalidDate)
        {
            throw new InvalidReservationDateException(reservation.Date.Value.Date);
        }

        var dateCapacity = _reservations.Where(x => x.Date == reservation.Date).Sum(x => x.Capacity);
        if (dateCapacity + reservation.Capacity > Capacity)
        {
            throw new ParkingSpotCapacityExceededException(Id);
        }

        _reservations.Add(reservation);
    }
    public void RemoveReservation(ReservationId id)
        => _reservations.RemoveWhere(x => x.Id == id);
    
    public void RemoveReservations(IEnumerable<Reservation> reservations)
        => _reservations.RemoveWhere(x => reservations.Any(r => r.Id == x.Id));
}