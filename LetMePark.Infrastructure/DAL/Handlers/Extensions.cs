﻿using LetMePark.Api.DTO;
using LetMePark.Core.Entities;

namespace LetMePark.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static WeeklyParkingSpotDto AsDto(this WeeklyParkingSpot entity)
        => new()
        {
            Id = entity.Id.Value.ToString(),
            Name = entity.Name,
            Capacity = entity.Capacity,
            From = entity.Week.From.Value.DateTime,
            To = entity.Week.To.Value.DateTime,
            Reservations = entity.Reservations.Select(x => new ReservationDto()
            {
                Id = x.Id,
                ParkingSpotId = x.ParkingSpotId,
                EmployeeName = x is VehicleReservation vr ? vr.EmployeeName : null,
                Type = x is VehicleReservation ? "vehicle" : "cleaning",
                Date = x.Date.Value.Date
            })
        };
}