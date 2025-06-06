using Microsoft.EntityFrameworkCore;
using System.IO;

public class AppDbContext : DbContext
{
    public DbSet<Eleccion> Elecciones { get; set; }
    public DbSet<Votante> Votantes { get; set; }

    private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, "elecciones.db");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Filename={DbPath}");
    }
}
