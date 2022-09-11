using LetMePark.Api.Commands;
using LetMePark.Api.DTO;

namespace LetMePark.Api.Services;

public interface IReservationService
{
    ReservationDto Get(Guid id);
    IEnumerable<ReservationDto> GetAllWeekly();
    Guid? Create(CreateReservation command);
    bool Update(ChangeReservationLicensePlate command);
    bool Delete(DeleteReservation command);
}