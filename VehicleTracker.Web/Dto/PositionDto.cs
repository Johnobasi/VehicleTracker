using System.ComponentModel.DataAnnotations;

namespace VehicleTracker.Web.Dto
{
    public class PositionRequestDto
    {   
        [Required]
        public int VehicleId { get; set; }

        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longitude { get; set; } 
    }

    public class PositionResponseDto
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

}
