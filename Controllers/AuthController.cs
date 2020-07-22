using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace jwttest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        [Route("gettoken")]
        public string GetToken()
        {
            IEnumerable<Claim> claims = new List<Claim> {
                new Claim("Id", "1"),
                new Claim("Name", "name222")
            };

            var nbf = DateTime.UtcNow;
            var exp = nbf.AddSeconds(10000);
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Jwt.JwtDefaultParameter.SecretCredentials));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(issuer: Jwt.JwtDefaultParameter.Issurer, audience: Jwt.JwtDefaultParameter.Audience,
            claims: claims, notBefore: nbf, expires: exp, signingCredentials: signingCredentials);

            var jwtHandler = new JwtSecurityTokenHandler();

            var token = jwtHandler.WriteToken(jwt);

            return token;
        }
    }
}