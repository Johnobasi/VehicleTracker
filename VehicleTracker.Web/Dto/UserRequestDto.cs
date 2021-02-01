using System.ComponentModel.DataAnnotations;

namespace VehicleTracker.Web.Dto
{
    public class UserRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }

    public class UserResponseDto
    { 
        public string UserName { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Token { get; set; }
    }

    public class UserLoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    
    }
}
