using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthCore.Helpers;
using AuthCore.Models;
using Microsoft.IdentityModel.Tokens;

namespace AuthCore.Services;

public class AuthService
{
    public string GenerateToken(User user) //naša metoda
    {
        var handler = new JwtSecurityTokenHandler(); //razred JwtSecurityTokenHandler je za generiranj
        //preverjanje in upravljanje z žetoni
        //razred ima CreateToken() metodo in WriteToken(token) metodo
        var key = Encoding.ASCII.GetBytes(AuthSettings.PrivateKey);
        //poišče ključ in ga spremeni v zaporedje bytov
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);
//ustveri objekt za podpis po metodi, ki smo jo določili
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = credentials,
        };
//opis žetona vsebuje podatke potrebne za ustvarjanje žetona
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(User user) //generira objekt ClainsIdentity za določenega userja
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));

        foreach (var role in user.Roles)
            claims.AddClaim(new Claim(ClaimTypes.Role, role));

        return claims;
    }
}