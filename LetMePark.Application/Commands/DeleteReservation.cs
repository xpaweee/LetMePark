using LetMePark.Application.Abstractions;

namespace LetMePark.Api.Commands
{
    public record DeleteReservation(Guid ReservationId) : ICommand;
}