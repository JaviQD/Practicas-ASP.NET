using System.ComponentModel.DataAnnotations;

namespace Practicas_ASP.NET.Models;

public partial class AuthRegistro
{
    public Guid JwtId { get; set; }

    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    [Display(Name = "Nombre de usuario")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "El correo electronico es obligatorio.")]
    [Display(Name = "Correo electronico")]
    public string? Mail { get; set; }

    [Required(ErrorMessage = "La clave es obligatoria.")]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    public DateOnly? AuditFechaGeneracion { get; set; }
}
