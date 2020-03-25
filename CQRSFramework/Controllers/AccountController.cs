using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CQRSFramework.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRSFramework.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AccountController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public IActionResult Login(string user, string password)
        {
            var usersClaims = new[]
            {
                new Claim(ClaimTypes.Name, "Bahar"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role,"CanDeactiveUser"),
                new Claim(ClaimTypes.Role,"CanCreateUser"),
                new Claim(ClaimTypes.Role,"CanEditUser"),
                new Claim(ClaimTypes.Role,"CanViewUser"),
            };
            if (user == "!!!!")
            {
                return BadRequest();
            }
            var jwtToken = _tokenService.GenerateAccessToken(usersClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            //            return new Json(new {Token= jwtToken});

            return new ObjectResult(new
            {
                token = jwtToken,
                refreshToken = refreshToken
            });
        }

        public IActionResult RefreshToken(string token, string refreshToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            //            var user = _usersDb.Users.SingleOrDefault(u => u.Username == username);
            //            if (user == null || user.RefreshToken != refreshToken)
            //return BadRequest();

            var newJwtToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            //            user.RefreshToken = newRefreshToken;
            //            await _usersDb.SaveChangesAsync();

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            });
        }
    }
}