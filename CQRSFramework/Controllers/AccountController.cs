﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CQRSFramework.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRSFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AccountController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public IActionResult Login(string user,string password)
        {
            var usersClaims = new[]
            {
                new Claim(ClaimTypes.Name, "Bahar"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim("CanCreateUser","true"), 
                new Claim("CanDeactiveUser","true"),
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
    }
}