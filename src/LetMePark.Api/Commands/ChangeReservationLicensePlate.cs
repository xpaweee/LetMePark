namespace LetMePark.Api.Commands;

public record ChangeReservationLicensePlate(Guid ReservationId, string LicensePlate);
