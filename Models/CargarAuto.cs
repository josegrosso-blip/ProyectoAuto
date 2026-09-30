using  System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ProyectoAuto.Models;

public class CargaAuto

{
    [Key]
    public int AutosId { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public int Año { get; set; }
    public string? Patente { get; set; }

    [JsonRequired]
    public int Kms { get; set; }
    public DateTime FechaDeIngreso { get; set; }
    public bool? Disponibilidad { get; set; }
}


