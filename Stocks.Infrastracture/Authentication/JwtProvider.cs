using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Stocks.Domain.Entities;
using Stocks.Application.Abstractions.Authentication;

namespace Stocks.Infrastracture.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions jwtOptions;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        jwtOptions = options.Value;
    }

    public string Generate(AppUser user)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Name,  user.FirstName!),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName!)
        };

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims,
            null,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}