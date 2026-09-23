namespace ProyectoAutos.Models;

public class CargaAuto

{
    public int AutoId { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public int Anio { get; set; }
    public string? Patente { get; set; }
    public int Kms { get; set; }
    public DateTime FechaIngreso { get; set; }

    public Estado Disponible { get; set; }
}

public enum Estado
{
    Disponible, No_Disponible
}