using Microsoft.IdentityModel.Tokens;
using Practicas_ASP.NET.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Practicas_ASP.NET.Methods
{
    public class Jwt : Controller
    {
        private readonly RegistroContext _context;
        private Encriptar encript = new Encriptar();
        private RandomKey _securityKey = new RandomKey();

        public Jwt(RegistroContext context) 
        {
            _context = context;
        }

        public string GenerarToken(string username, string password)
        {
            // Verificar si el usuario y la contraseña son válidos
            var user = _context.AuthRegistros
                                .FirstOrDefault(u => u.Username == username && u.Password == encript.EncryptarPassword(password));

            if (user == null)
            {
                // Usuario o contraseña inválidos, devolver null o lanzar una excepción, según tus necesidades.
                return null;
            }

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

        public string GetToken(string username, string mail, string password)
        {
            // Verificar si el usuario, el correo y la clave son validos
            var user = _context.AuthRegistros
                                .FirstOrDefault(u => u.Username == username && 
                                                     u.Password == encript.EncryptarPassword(password) &&
                                                     u.Mail == mail);

            if (user == null)
            {
                // Usuario o contraseña inválidos, devolver null o lanzar una excepción, según tus necesidades.
                return null;
            }

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
                expires: DateTime.Now.AddMinutes(35), // Tiempo de expiración del token (30 minutos en este ejemplo)
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
