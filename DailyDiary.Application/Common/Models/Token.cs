using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DailyDiary.Application.Common.Models;

public static class Token
{
    public static string Create(IConfiguration config)
    {
        var _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var _issuer = config["Jwt:Issuer"];
        var _audience = config["Jwt:Audience"];

        var signinCredentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);
        
        var tokeOptions = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: new List<Claim>(),
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signinCredentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return tokenString;
    }
}