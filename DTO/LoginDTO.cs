
using System.ComponentModel.DataAnnotations;

namespace CandyApi.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public  string? Username { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public  string? Password { get; set; }

}
