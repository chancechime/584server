using AngularApp1.Server.DTO;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IdentityModel.Tokens.Jwt;

namespace AngularApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<AppUser> userManager, JwtHandler jwtHandler) : ControllerBase
    {
        //private readonly JwtHandler jwtHandler = jwtHandler;

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync(LoginRequest request)
        {
            AppUser? user = await userManager.FindByNameAsync(request.Username);

            // 401 - Unauthorized
            // 403 - Forbidden (Doesn't want to Authorize)
            if (user == null){return Unauthorized("Bad Username 👻👻👻👻👻👻👻👻👻👻👻👻👻");} // no user

            bool success = await userManager.CheckPasswordAsync(user, request.Password);

            if (!success) { return Unauthorized("Bad Password... Loser"); } // just.. bad

            JwtSecurityToken token = await jwtHandler.GetTokenAsync(user);

            var jwtString = new JwtSecurityTokenHandler().WriteToken(token);

            LoginResponse response = new()
            {
                Success = true,
                Message = "Mama loves you❤",
                Token = jwtString
            };

            return Ok(response);
        }
    }
}
