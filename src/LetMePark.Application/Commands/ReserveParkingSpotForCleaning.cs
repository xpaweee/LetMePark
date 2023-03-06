using LetMePark.Application.Abstractions;

namespace LetMePark.Api.Commands;

public record ReserveParkingSpotForCleaning(DateTime Date) : ICommand;
