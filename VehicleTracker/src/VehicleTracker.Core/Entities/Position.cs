using System;
using System.Collections.Generic;
using VehicleTracker.SharedKernel;

namespace VehicleTracker.Core.Entities
{
    public partial class Position:BaseEntity
    {
      
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? Entry { get; set; }
       
        public Vehicles Vehicles { get; set; }
    }
}
