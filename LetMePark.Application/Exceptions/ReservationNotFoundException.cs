using LetMePark.Core.Exceptions;

namespace LetMePark.Application.Exceptions;


public sealed class ReservationNotFoundException : CustomException
{
    public Guid Id { get; }

    public ReservationNotFoundException(Guid id) 
        : base($"Reservation with ID: {id} was not found.")
    {
        Id = id;
    }
}