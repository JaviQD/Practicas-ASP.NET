using Microsoft.IdentityModel.Tokens;
using Practicas_ASP.NET.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Practicas_ASP.NET.Services.JwtSevices
{
    public class Jwt : Controller, IJwt
    {
        private readonly RegistroContext _context;

        public Jwt(RegistroContext context)
        {
            _context = context;
        }

        public string GenerarRandomKey(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var randomBytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = validChars[randomBytes[i] % validChars.Length];
            }

            return new string(result);
        }

        public string GenerarToken(string username, string password)
        {
            var user = ObtenerUsuario(username, password);
            if (user is null) return null;

            var token = CrearToken(username);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetToken(string username, string mail, string password)
        {
            // Verificar si el usuario, el correo y la clave son validos
            var user = ObtenerUsuarioWithMail(username, password, mail);

            if (user is null) return null;

            var token = CrearToken(username);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string EncryptarPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public AuthRegistro ObtenerUsuario(string username, string password)
        {
            return _context.AuthRegistros.FirstOrDefault(u => 
                                                         u.Username == username &&
                                                         u.Password == EncryptarPassword(password));
        }

        public AuthRegistro ObtenerUsuarioWithMail(string username, string password,string mail)
        {
            return _context.AuthRegistros.FirstOrDefault(u =>
                                                         u.Username == username &&
                                                         u.Password == EncryptarPassword(password) &&
                                                         u.Mail == mail);
        }

        public JwtSecurityToken CrearToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GenerarRandomKey(32)));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            return new JwtSecurityToken
            (
                issuer: "LocaltHost43880",
                audience: "Today",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
        }
    }
}
