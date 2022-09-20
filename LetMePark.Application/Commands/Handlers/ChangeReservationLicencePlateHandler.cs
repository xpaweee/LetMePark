using LetMePark.Api.Commands;
using LetMePark.Application.Abstractions;
using LetMePark.Application.Exceptions;
using LetMePark.Core.Entities;
using LetMePark.Core.Repository;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Application.Commands.Handlers;


    
    public sealed class ChangeReservationLicencePlateHandler : ICommandHandler<ChangeReservationLicensePlate>
    {
        private readonly IWeeklyParkingSpotRepository _repository;

        public ChangeReservationLicencePlateHandler(IWeeklyParkingSpotRepository repository)
            => _repository = repository;

        public async Task HandleAsync(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservation(command.ReservationId);
            if (weeklyParkingSpot is null)
            {
                throw new WeeklyParkingSpotNotFoundException(command.ReservationId);
            }

            var reservationId = new ReservationId(command.ReservationId);
            var reservation = weeklyParkingSpot.Reservations
                .OfType<VehicleReservation>()
                .SingleOrDefault(x => x.Id == reservationId);

            if (reservation is null)
            {
                throw new ReservationNotFoundException(command.ReservationId);
            }
    
            reservation.ChangeLicensePlate(command.LicensePlate);
            await _repository.UpdateAsync(weeklyParkingSpot);
        }
    
        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservation(ReservationId id)
            => (await _repository.GetAllAsync())
                .SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));
    }
