

namespace CandyApi.Entities;

public class Vendedor
{
    public int Id { get; set; }
    public string? Nombre { get; set; } = null!;
    public string? Correo { get; set; } = null;
    public string? Puesto { get; set; } = null;
    public string? Agente { get; set; } = null;

}
