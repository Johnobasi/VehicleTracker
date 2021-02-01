using System;
using System.Collections.Generic;

namespace VehicleTracker.Web.Dto
{
    public class VehicleRequestDto
    {
        public string Name { get; set; }
        public string Number { get; set; }     
    }

    public class VehicleResponseDto
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public virtual ICollection<PositionResponseDto> Postion { get; set; }
    }
}