using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practicas_ASP.NET.Models;

public partial class AuthRegistro
{
    public Guid JwtId { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Mail { get; set; }

    [Required]
    public string? Password { get; set; }

    public DateOnly? AuditFechaGeneracion { get; set; }
}
