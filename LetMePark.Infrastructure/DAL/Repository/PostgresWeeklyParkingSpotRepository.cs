using LetMePark.Core.Entities;
using LetMePark.Core.Repository;
using LetMePark.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMePark.Infrastructure.DAL.Repository
{
    internal class PostgresWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private readonly LetMeParkDbContext _dbContext;

        public PostgresWeeklyParkingSpotRepository(LetMeParkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            await _dbContext.AddAsync(weeklyParkingSpot);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Remove(weeklyParkingSpot);
            await _dbContext.SaveChangesAsync();
        }

        public Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id)
        {
            return _dbContext.WeeklyParkingSpots
                .Include(x => x.Reservations)
                .SingleOrDefaultAsync(x => x.Id == id);
        } 
        
        public async Task<IEnumerable<WeeklyParkingSpot>>  GetByWeekAsync(Week week)
        {
            return await _dbContext.WeeklyParkingSpots
                .Include(x => x.Reservations)
                .Where(x => x.Week == week)
                .ToListAsync();
        }

        public async Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync()
        {
            var result = await _dbContext.WeeklyParkingSpots
                .Include(x => x.Reservations)
                .ToListAsync();

            return result.AsEnumerable();
        }

        public async Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Update(weeklyParkingSpot);
            await _dbContext.SaveChangesAsync();
        }
    }
}
