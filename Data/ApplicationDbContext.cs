using Microsoft.EntityFrameworkCore;
using ProyectoAuto.Models;

namespace ProyectoAutos.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<CargaAuto> CargaAuto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CargaAuto>()
            .Property(auto => auto.FechaDeIngreso)
            .HasColumnName("FechaIngreso");

        modelBuilder.Entity<CargaAuto>()
            .Property(auto => auto.Disponibilidad)
            .HasColumnName("Disponible");
    }
}