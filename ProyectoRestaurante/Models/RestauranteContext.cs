using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoRestaurante.Models;

public partial class RestauranteContext : DbContext
{
    public RestauranteContext()
    {
    }

    public RestauranteContext(DbContextOptions<RestauranteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Detallespedido> Detallespedido { get; set; }

    public virtual DbSet<Estado> Estado { get; set; }

    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<Pedido> Pedido { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=juanes1;database=Restaurante", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Detallespedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("detallespedido");

            entity.HasIndex(e => e.IdMenu, "Fk_DetallesPedido_Menu");

            entity.HasIndex(e => e.IdPedido, "Fk_DetallesPedido_Pedido");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.PrecioPlatillo).HasPrecision(5, 2);

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.Detallespedido)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("Fk_DetallesPedido_Menu");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Detallespedido)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("Fk_DetallesPedido_Pedido");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estado");

            entity.Property(e => e.Nombre).HasMaxLength(15);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("menu");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Nombre).HasMaxLength(40);
            entity.Property(e => e.Precio).HasPrecision(5, 2);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pedido");

            entity.HasIndex(e => e.IdEstado, "Fk_PedidoEstado");

            entity.HasIndex(e => e.IdUsuario, "Fk_PedidoUsuario");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnType("datetime");
            entity.Property(e => e.IdEstado).HasDefaultValueSql("'1'");
            entity.Property(e => e.MontoPagar).HasPrecision(10, 2);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Pedido)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("Fk_PedidoEstado");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pedido)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PedidoUsuario");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.Nombre).HasMaxLength(25);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Correo, "Correo").IsUnique();

            entity.HasIndex(e => e.IdRol, "Fk_UsuarioRol");

            entity.Property(e => e.Contrasena).HasMaxLength(25);
            entity.Property(e => e.Correo).HasMaxLength(45);
            entity.Property(e => e.Nombre).HasMaxLength(45);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("Fk_UsuarioRol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
