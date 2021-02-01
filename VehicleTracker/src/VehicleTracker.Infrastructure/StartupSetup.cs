
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VehicleTracker.Core.Entities;
using VehicleTracker.Core.Interfaces;
using VehicleTracker.Infrastructure.Data;

namespace VehicleTracker.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddVehicleDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString)); // will be created in web project root
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IPositionReppository, PositionRepository>();
        }
     
    }
}
