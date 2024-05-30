using System.ComponentModel.DataAnnotations;

namespace Practicas_ASP.NET.Models;

public partial class AuthRegistro
{
    public Guid JwtId { get; set; }

    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    [Display(Name = "Nombre de usuario")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El nombre de usuario solo puede contener letras y no debe contener espacios.")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "El correo electronico es obligatorio.")]
    [Display(Name = "Correo electronico")]
    [EmailAddress(ErrorMessage = "El campo Correo debe ser una dirección de correo electrónico válida")]
    public string? Mail { get; set; }

    [Required(ErrorMessage = "La Contraseña es obligatoria.")]
    [Display(Name = "Contraseña")]
    [RegularExpression(@"^[^\s]+$", ErrorMessage = "La contraseña no puede contener espacios.")]
    public string? Password { get; set; }

    public DateOnly? AuditFechaGeneracion { get; set; }
}
