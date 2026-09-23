using Microsoft.EntityFrameworkCore;
using ProyectoAutos.Models;

namespace ProyectoAutos.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<CargaAuto> CargaAuto { get; set; }
   
}