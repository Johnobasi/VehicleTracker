using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Core.Entities;

namespace VehicleTracker.Core.Interfaces
{
   public interface IJwtSecurity
    {
        string CreateToken(User user, string userRole);
    }
}
