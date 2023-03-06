namespace LetMePark.Core.Exceptions;

public sealed class ParkingSportAlreadyReservedException : CustomException
{
    public DateTime DateTime { get; }
    public string Name { get; set; }

    public ParkingSportAlreadyReservedException(DateTime dateTime, string name) : base($"Reservation spot: {name} is alredy reserved at {dateTime.Date} ")
    {
        DateTime = dateTime;
        Name = name;
    }
}