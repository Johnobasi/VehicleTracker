using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.Infrastructure.RolesManager
{

    public static class RolesData
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            List<string> roleList = new List<string>()
            {
                "Admin",
                "User"
            };

            foreach (var role in roleList)
            {
                var result = roleManager.RoleExistsAsync(role).Result;
                if (!result)
                {
                    roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
