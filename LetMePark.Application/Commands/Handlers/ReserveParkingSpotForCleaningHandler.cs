using LetMePark.Api.Commands;
using LetMePark.Application.Abstractions;
using LetMePark.Core.DomainServices;
using LetMePark.Core.Repository;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Application.Commands.Handlers;

public sealed class ReserveParkingSpotForCleaningHandler : ICommandHandler<ReserveParkingSpotForCleaning>
{
    private readonly IWeeklyParkingSpotRepository _repository;
    private readonly IParkingReservationService _reservationService;

    public ReserveParkingSpotForCleaningHandler(IWeeklyParkingSpotRepository repository, 
        IParkingReservationService reservationService)
    {
        _repository = repository;
        _reservationService = reservationService;
    }
    
    public async Task HandleAsync(ReserveParkingSpotForCleaning command)
    {
        var week = new Week(command.Date);
        var weeklyParkingSpots = (await _repository.GetByWeekAsync(week)).ToList();

        _reservationService.ReserveParkingForCleaning(weeklyParkingSpots, new Date(command.Date));

        var tasks = weeklyParkingSpots.Select(x => _repository.UpdateAsync(x));
        await Task.WhenAll(tasks);

        // foreach (var weeklyParkingSpot in weeklyParkingSpots)
        // {
        //     await _repository.UpdateAsync(weeklyParkingSpot);
        // }
    }
}