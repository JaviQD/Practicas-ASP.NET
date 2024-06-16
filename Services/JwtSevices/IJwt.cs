using Practicas_ASP.NET.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Practicas_ASP.NET.Services.JwtSevices
{
    public interface IJwt
    {
        string GenerarRandomKey(int length);

        string GenerarToken(string username, string password);

        string GetToken(string username, string mail, string password);

        string EncryptarPassword(string password);

        AuthRegistro ObtenerUsuario(string username, string password);

        AuthRegistro ObtenerUsuarioWithMail(string username, string password, string Email);

        JwtSecurityToken CrearToken(string username);
    }
}
