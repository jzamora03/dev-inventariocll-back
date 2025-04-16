using Microsoft.EntityFrameworkCore;
using InventarioAPI.Models;

namespace InventarioAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<MovimientoInventario> MovimientosInventario { get; set; }
}
