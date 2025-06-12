using Microsoft.IdentityModel.Tokens;
using MoneyOrbit.Application.Interfaces.IServices; // Add this
using MoneyOrbit.Core.Entities;
using System.IdentityModel.Tokens.Jwt; // Use the standard handler
using System.Security.Claims;
using System.Text;

// Make the class public and implement the interface
public sealed class JwtTokenProvider : IJwtTokenProvider
{
    private readonly IConfiguration _configuration;

    // Use IConfiguration directly
    public JwtTokenProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Create(User user)
    {
        string secretKey = _configuration["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature); // Use a strong algorithm

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.ID),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            // Add any other claims you need, like AccessLevel
            new Claim(ClaimTypes.Role, user.AccessLevel)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}