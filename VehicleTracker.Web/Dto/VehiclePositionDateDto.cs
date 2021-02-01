using System;
using System.ComponentModel.DataAnnotations;
using VehicleTracker.Web.Validator;

namespace VehicleTracker.Web.Dto
{
    public class VehiclePositionDateDto
    {
        [Required]
        public int VehicleId { get; set; }
       
        [DateLessThan("EndDate", ErrorMessage = "Start date can not be greater than end date")]
        public DateTime? StartDate { get; set; }
      
        public DateTime?  EndDate{ get; set; }
    }
}
