using DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AngularApp1.Server
{
    public class JwtHandler(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        // OpenID
        public async Task<JwtSecurityToken> GetTokenAsync(AppUser user) => new (
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: await GetClaimsAsync(user),
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["JwtSettings:ExpirationTimeInMinutes"])),
            signingCredentials: GetSigningCredentials());

        private SigningCredentials GetSigningCredentials()
        {
            byte[] key = Encoding.UTF8.GetBytes(configuration["JwtSettings:SecurityKey"]!); // similar to encyrpt, generates
            SymmetricSecurityKey secret = new(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(AppUser user)
        {
            List<Claim> claims = [new Claim(ClaimTypes.Name, user.UserName!)];
            claims.AddRange(from role in await userManager
                            .GetRolesAsync(user) // gets data from ASPNet table
                            select new Claim(ClaimTypes.Role, role));
            return claims;
        }
    }
}
