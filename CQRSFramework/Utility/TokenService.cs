using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CQRSFramework.Utility
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["serverSigningPassword"]));

            using (RSA privateRsa = RSA.Create())
            {
                var tt = Path.Combine(Directory.GetCurrentDirectory(),
                    "Keys",
                    this._configuration.GetValue<String>("PrivateKey")
                );
                privateRsa.FromXmlFile(tt);
                var privateKey = new RsaSecurityKey(privateRsa);
                SigningCredentials signingCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);

                var jwtToken = new JwtSecurityToken(issuer: "Bahar",
                    audience: "Anyone",
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["accessTokenDurationInMinutes"])),
                    signingCredentials: signingCredentials
                );

                return new JwtSecurityTokenHandler().WriteToken(jwtToken);

//                var jwt = new JwtSecurityToken(
//                    signingCredentials: signingCredentials,
//                    claims: claims,
//                    notBefore: utcNow,
//                    expires: utcNow.AddSeconds(this.configuration.GetValue<int>("Tokens:Lifetime")),
//                    audience: this.configuration.GetValue<String>("Tokens:Audience"),
//                    issuer: this.configuration.GetValue<String>("Tokens:Issuer")
//                );
//
//                return new JwtSecurityTokenHandler().WriteToken(jwt);
            }


            
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["serverSigningPassword"])),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}