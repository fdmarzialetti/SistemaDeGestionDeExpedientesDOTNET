using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion;
namespace SGE.Repositorios;

public class SGEContext : DbContext
{
    public DbSet<Expediente> Expedientes { get; set; }
    public DbSet<Tramite> Tramites { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder
    optionsBuilder)
    {
        // Ruta del directorio de ejecución
        var basePath = AppContext.BaseDirectory;
        // Construir la ruta relativa hacia SGE.Repositorio
        var dbDirectory = Path.Combine(basePath, "..","..","..","..","SGE.Repositorios");
        // Ruta completa de la base de datos
        var dbPath = Path.Combine(dbDirectory, "SGE.sqlite");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}