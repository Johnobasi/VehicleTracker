using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using VehicleTracker.Core.Entities;
using VehicleTracker.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;

namespace VehicleTracker.Infrastructure.Data
{
    public  class VehicleRepository:EfRepository,IVehicleRepository
    {
        private readonly ILogger<VehicleRepository> _logger;
        private readonly AppDbContext _context;

        public VehicleRepository(AppDbContext dbContext,ILogger<VehicleRepository> logger):base(dbContext,logger)
        {
            _logger = logger;
            _context = dbContext;
        }

        public async Task<Vehicles> GetVehicleByNumber(string number)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(r => r.Number.Trim().ToLower() == number.Trim().ToLower());
        }

        public async Task<Vehicles> GetVehicles(int id, DateTime? startDate, DateTime? endDate,bool includePositions)
        {
            if (includePositions)
            {
                if (startDate.HasValue && endDate.HasValue)
                {
                    var vehicle = await _context.Vehicles.Include(r => r.Positions).FirstOrDefaultAsync(r => r.Id == id);
                    vehicle.Positions = vehicle.Positions.Where(r => r.Entry.Value >= startDate && r.Entry.Value <= endDate).ToList();

                }
                    return await _context.Vehicles.Include(r => r.Positions).FirstOrDefaultAsync(r => r.Id == id);

            }
            return await _context.Vehicles.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
