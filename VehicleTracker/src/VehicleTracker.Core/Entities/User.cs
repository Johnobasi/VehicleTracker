
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.Core.Entities
{
    public class User: IdentityUser
    {
 
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
