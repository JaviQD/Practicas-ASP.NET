using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practicas_ASP.NET.Methods
{
    public class Jwt
    {
        private RandomKey _securityKey = new RandomKey();
        public string GenerarToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey.GenerarRandomKey(32)));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Identificador único del token
            };

            var token = new JwtSecurityToken(
                issuer: "LocaltHost43880", // Emisor del token
                audience: "Today", // Audiencia del token
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Tiempo de expiración del token (30 minutos en este ejemplo)
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
