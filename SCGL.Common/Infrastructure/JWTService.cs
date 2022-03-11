using Lazarus.Common.Authentication;
using Lazarus.Common.Model;
using Lazarus.Common.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
namespace Lazarus.Common.Infrastructure
{
    public class JWTService : IJWTService
    {
        public string GenerateJWT(UserCredential user)
        {
            var claims = new List<Claim>
                    {
                                       new Claim(ClaimTypes.Name, user.Email),
                     new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", user.Email),
                     new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", user.UserId.ToString()),
                     new Claim(ClaimStore.Email.ToString(), user.Email),
                     new Claim(ClaimStore.UserId.ToString(), user.UserId),
                     new Claim(ClaimStore.Role.ToString(), user.Roles.ToJSON()),
                     new Claim(ClaimStore.Domain.ToString(), user.Domain),
        };
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfigUtilities.GetAppConfig<string>("JwtKey")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble("8"));

            var token = new JwtSecurityToken(
                issuer: AppConfigUtilities.GetAppConfig<string>("JwtIssuer"),
                audience: AppConfigUtilities.GetAppConfig<string>("JwtAudience"),
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            string t = new JwtSecurityTokenHandler().WriteToken(token);


            return t;
        }
    }
}
