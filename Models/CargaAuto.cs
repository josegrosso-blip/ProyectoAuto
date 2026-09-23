using  System.ComponentModel.DataAnnotations;
namespace ProyectoAutos.Models;

public class CargaAuto

{
    [Key]
    public int AutoId { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public int Año { get; set; }
    public string? Patente { get; set; }
    public int Kms { get; set; }
    public DateTime FechaIngreso { get; set; }
    public bool Disponible { get; set; }
}

