using LetMePark.Api.Commands;
using LetMePark.Api.DTO;
using LetMePark.Core.Abstractions;
using LetMePark.Core.DomainServices;
using LetMePark.Core.Entities;
using LetMePark.Core.Repository;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Api.Services
{
    public class ReservationService : IReservationService
    {
        private static IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
        private readonly IParkingReservationService _parkingReservationService;


        public ReservationService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository, IParkingReservationService parkingReservationService)
        {
            _clock = clock;
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
            _parkingReservationService = parkingReservationService;
        }


        public async Task<ReservationDto> GetAsync(Guid id)
        {
            var reservations = await GetAllWeeklyAsync();
            return reservations.SingleOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync()
        {
            var weeklyParkingSpots = await _weeklyParkingSpotRepository.GetAllAsync();
            
            return weeklyParkingSpots.SelectMany(x => x.Reservations).Select(x => new ReservationDto()
            {
                Id = x.Id,
                Date = x.Date.Value.Date,
                EmployeeName = x is VehicleReservation vehicleReservation? vehicleReservation.EmployeeName : string.Empty,
                ParkingSpotId = x.ParkingSpotId
            });
        }

        public async Task<Guid?> ReserveForVehicleAsync(ReserveParkingSpotForVehicle command)
        {
           
        }

        public async Task ReserveForCleaningAsync(ReserveParkingSpotForCleaning command)
        {
            var week = new Week(command.Date);
            var weeklkyParkingSpots = (await _weeklyParkingSpotRepository.GetByWeekAsync(week)).ToList();
            
            _parkingReservationService.ReserveParkingForCleaning(weeklkyParkingSpots, new Date(command.Date));

            // var tasks = weeklkyParkingSpots.Select(x => _weeklyParkingSpotRepository.UpdateAsync(x));
            // await Task.WhenAll(tasks);

            foreach (var parkingSpot in weeklkyParkingSpots)
            {
                await _weeklyParkingSpotRepository.UpdateAsync(parkingSpot);
            }
        }

        public async Task<bool> ChangeReservationLicensePlateAsync(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);

            if (weeklyParkingSpot is null)
            {
                return false;
            }

            var reservationId = new ReservationId(command.ReservationId);

            var existingReservation = weeklyParkingSpot.Reservations.OfType<VehicleReservation>().SingleOrDefault(x => x.Id == reservationId);

            if (existingReservation is null)
            {
                return false;
            }


            if (existingReservation.Date.Value.Date <= _clock.Current())
            {
                return false;
            }

            existingReservation.ChangeLicensePlate(command.LicensePlate);
            await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
            
            return true;
        }


        public async Task<bool> DeleteAsync(DeleteReservation command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot is null)
            {
                return false;
            }

            var reservationId = new ReservationId(command.ReservationId);

            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == reservationId);

            if (existingReservation is null)
            {
                return false;
            }

            weeklyParkingSpot.RemoveReservation(command.ReservationId);
            await _weeklyParkingSpotRepository.DeleteAsync(weeklyParkingSpot);
            return true;
        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(ReservationId commandReservationId)
        {
            var weeklyParkingSpot = await _weeklyParkingSpotRepository.GetAllAsync();
            
            return weeklyParkingSpot.SingleOrDefault(x => x.Reservations.Any(r => r.Id == commandReservationId));
        }
            

    }
}