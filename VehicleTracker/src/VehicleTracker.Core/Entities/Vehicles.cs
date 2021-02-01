using System;
using System.Collections.Generic;
using VehicleTracker.SharedKernel;

namespace VehicleTracker.Core.Entities
{
    public partial class Vehicles:BaseEntity
    {
       
        public string Name { get; set; }
        public string Number { get; set; }
        public DateTime? Entry { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }
}
