using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Core.Entities;
using VehicleTracker.SharedKernel.Interfaces;

namespace VehicleTracker.Core.Interfaces
{
     public  interface IVehicleRepository:IRepository
    {
        Task<Vehicles> GetVehicleByNumber(string Number);
        Task<Vehicles> GetVehicles(int id, DateTime? startDate, DateTime? endDate,bool includePositions = false);
       
    }
}
