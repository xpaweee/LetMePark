using LetMePark.Api.Commands;
using LetMePark.Application.Abstractions;
using LetMePark.Application.Exceptions;
using LetMePark.Core.Entities;
using LetMePark.Core.Repository;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Application.Commands.Handlers;

public sealed class DeleteReservationHandler : ICommandHandler<DeleteReservation>
{
    private readonly IWeeklyParkingSpotRepository _repository;

    public DeleteReservationHandler(IWeeklyParkingSpotRepository repository)
        => _repository = repository;
    
    public async Task HandleAsync(DeleteReservation command)
    {
        var weeklyParkingSpot = await GetWeeklyParkingSpotByReservation(command.ReservationId);
        if (weeklyParkingSpot is null)
        {
            throw new WeeklyParkingSpotNotFoundException(command.ReservationId);
        }

        weeklyParkingSpot.RemoveReservation(command.ReservationId);
        await _repository.UpdateAsync(weeklyParkingSpot);
    }
    
    private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservation(ReservationId id)
        => (await _repository.GetAllAsync())
            .SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));
}