using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehicleTracker.Core.Entities;
using VehicleTracker.Core.Interfaces;

namespace Plugins.JwtHandler
{
    public class JwtSecurity : IJwtSecurity
    {
        private readonly IConfiguration _configuration;

        public JwtSecurity(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(User user, string userRole)
        {

            var claim = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(ClaimTypes.Role,userRole ??"User")


            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("jwtHandler").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims: claim),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var returnToken = tokenHandler.WriteToken(token);

            return returnToken;
        }

       
    }
}