using System.Dynamic;
using Api.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<UsuarioDb> Usuarios => Set<UsuarioDb>();
    public DbSet<EstadoProyectoDb> EstadosProyecto => Set<EstadoProyectoDb>();
    public DbSet<ProyectoDb> Proyectos => Set<ProyectoDb>();
    public DbSet<ColumnaDb> Columnas => Set<ColumnaDb>();
    public DbSet<PrioridadDb> Prioridades => Set<PrioridadDb>();
    public DbSet<TareaDb> Tareas => Set<TareaDb>();
    public DbSet<Proyecto_UsuarioDb> Proyecto_Usuario => Set<Proyecto_UsuarioDb>();

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

        modelBuilder.Entity<EstadoProyectoDb>(entity =>
        {
            entity.ToTable("EstadosProyecto");
            entity.HasKey(e => e.Codigo);
            entity.Property(e => e.Nombre).HasColumnName("Nombre").IsRequired();
            entity.Property(e => e.EstaActivo).HasColumnName("EstaActivo").IsRequired();
        });

        modelBuilder.Entity<ProyectoDb>(entity =>
        {
            entity.ToTable("Proyectos");
            entity.HasKey(e => e.Secuencial);
            entity.Property(e => e.Nombre).HasColumnName("Nombre").IsRequired();
            entity.Property(e => e.Descripcion).HasColumnName("Descripcion").IsRequired();
            entity.Property(e => e.FechaInicio).HasColumnName("FechaInicio").IsRequired();
            entity.Property(e => e.FechaFin).HasColumnName("FechaFin").IsRequired();
            entity.Property(e => e.CodigoEstadoProyecto).HasColumnName("CodigoEstadoProyecto").IsRequired();

            entity.HasOne(e => e.EstadoProyecto)
                .WithMany()
                .HasForeignKey(e => e.CodigoEstadoProyecto)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ColumnaDb>(entity =>
        {
            entity.ToTable("Columnas");
            entity.HasKey(e => e.Secuencial);
            entity.Property(e => e.Nombre).HasColumnName("Nombre").IsRequired();
            entity.Property(e => e.Orden).HasColumnName("Orden").IsRequired();
            entity.Property(e => e.SecuencialProyecto).HasColumnName("SecuencialProyecto").IsRequired();
            entity.Property(e => e.EstaActivo).HasColumnName("EstaActivo").IsRequired();

            entity.HasOne(e => e.Proyecto)
                .WithMany()
                .HasForeignKey(e => e.SecuencialProyecto)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PrioridadDb>(entity =>
        {
            entity.ToTable("Prioridades");
            entity.HasKey(e => e.Secuencial);
            entity.Property(e => e.Nombre).HasColumnName("Nombre").IsRequired();
            entity.Property(e => e.Color).HasColumnName("Color").IsRequired();
            entity.Property(e => e.EstaActivo).HasColumnName("EstaActivo").IsRequired();
        });

        modelBuilder.Entity<TareaDb>(entity =>
        {
            entity.ToTable("Tareas");
            entity.HasKey(e => e.Secuencial);
            entity.Property(e => e.Titulo).HasColumnName("Titulo").IsRequired();
            entity.Property(e => e.Descripcion).HasColumnName("Descripcion").IsRequired();
            entity.Property(e => e.SecuencialColumna).HasColumnName("SecuencialColumna").IsRequired();
            entity.Property(e => e.SecuencialPrioridad).HasColumnName("SecuencialPrioridad").IsRequired();
            entity.Property(e => e.SecuencialUsuarioAsignado).HasColumnName("SecuencialUsuarioAsignado").IsRequired();
            entity.Property(e => e.Orden).HasColumnName("Orden").IsRequired();
            entity.Property(e => e.EstaActivo).HasColumnName("EstaActivo").IsRequired();

            entity.HasOne(e => e.Columna)
                .WithMany()
                .HasForeignKey(e => e.SecuencialColumna)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Prioridad)
                .WithMany()
                .HasForeignKey(e => e.SecuencialPrioridad)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.UsuarioAsignado)
                .WithMany()
                .HasForeignKey(e => e.SecuencialUsuarioAsignado)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Proyecto_UsuarioDb>(entity =>
        {
            entity.ToTable("Proyecto_Usuario");
            entity.HasKey(e => e.Secuencial);
            entity.Property(e => e.SecuencialProyecto).HasColumnName("SecuencialProyecto").IsRequired();
            entity.Property(e => e.SecuencialUsuario).HasColumnName("SecuencialUsuario").IsRequired();
            entity.Property(e => e.EstaActivo).HasColumnName("EstaActivo").IsRequired();

            entity.HasOne(e => e.Proyecto)
                .WithMany()
                .HasForeignKey(e => e.SecuencialProyecto)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.SecuencialUsuario)
                .OnDelete(DeleteBehavior.Restrict);
        });

    }
}