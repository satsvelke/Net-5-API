using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Workflow.AppSettings;

namespace Workflow.Authentication
{
    public class JWTToken
    {
        private readonly IOptions<JwtSettings> jwtSettings;

        public JWTToken(IOptions<JwtSettings> jwtSettings) => this.jwtSettings = jwtSettings;
        public string GetToken(string userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Valid", "1"),
                    new Claim("UserId", userId)
                };

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(jwtSettings.Value.Issuer, //Issure    
                            jwtSettings.Value.Audience,  //Audience    
                            claims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
