using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Core.Interfaces;

namespace VehicleTracker.Infrastructure.Data
{
    public class PositionRepository:EfRepository, IPositionReppository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<PositionRepository> _logger;

        public PositionRepository(AppDbContext dbContext,ILogger<PositionRepository> logger):base(dbContext,logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
    }
}
