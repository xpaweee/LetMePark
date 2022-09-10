namespace LetMePark.Api.Exceptions;

public sealed class InvalidReservationDateException : CustomException
{
    public DateTime DateTime { get; }

    public InvalidReservationDateException(DateTime dateTime) : base($"Reservation date {dateTime.Date} is invalid")
    {
        DateTime = dateTime;
    }
}