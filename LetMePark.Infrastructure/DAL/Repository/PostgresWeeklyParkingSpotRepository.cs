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

        public void Add(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Add(weeklyParkingSpot);
            _dbContext.SaveChanges();
        }

        public void Delete(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Remove(weeklyParkingSpot);
            _dbContext.SaveChanges();
        }

        public WeeklyParkingSpot Get(ParkingSpotId id)
        {
            return _dbContext.WeeklyParkingSpots
                .Include(x => x.Reservations)
                .SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<WeeklyParkingSpot> GetAll()
        {
            return _dbContext.WeeklyParkingSpots
                .Include(x => x.Reservations)
                .ToList();
        }

        public void Update(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Update(weeklyParkingSpot);
            _dbContext.SaveChanges();
        }
    }
}
