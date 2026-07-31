using System.Dynamic;
using Api.Infrastructure.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistance;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<UsuarioDb> Usuarios => Set<UsuarioDb>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UsuarioDb>(entity =>
        {
            entity.ToTable("Usuarios");
            entity.HasKey(e => e.Secuencial);
            entity.Property(e => e.Nombre).HasColumnName("Nombre").IsRequired();
            entity.Property(e => e.Correo).HasColumnName("Correo").IsRequired();
            entity.Property(e => e.Password).HasColumnName("Password").IsRequired();
        });
    }
}