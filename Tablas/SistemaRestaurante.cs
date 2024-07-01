using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyecto__Final.EntidadProcedures;

namespace Proyecto__Final.Tablas;

public partial class SistemaRestaurante : DbContext
{
    public SistemaRestaurante()
    {
    }

    public SistemaRestaurante(DbContextOptions<SistemaRestaurante> options)
        : base(options)
    {
    }

    public virtual DbSet<CabezeraMaestro> CabezeraMaestros { get; set; }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClienteFuncione> ClienteFunciones { get; set; }

    public virtual DbSet<DetalleCarrito> DetalleCarritos { get; set; }

    public virtual DbSet<DetalleCarritoPersonalizado> DetalleCarritoPersonalizados { get; set; }

    public virtual DbSet<DetalleMaestro> DetalleMaestros { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<DetallePedidoPersonalizado> DetallePedidoPersonalizados { get; set; }

    public virtual DbSet<DetallePlato> DetallePlatos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<MesaFuncione> MesaFunciones { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Plato> Platos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<UsuarioLogin> UsuarioLogins { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-IU0VIP9; DataBase=SistemaRestaurante; user id= sa; pwd = 1234;Integrated Security=false; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CabezeraMaestro>(entity =>
        {
            entity.HasKey(e => e.IdCabezeraMaestro).HasName("PK__Cabezera__A3FCD483ADB659B4");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdCarrito).HasName("PK__Carrito__8B4A618C84703302");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Descuento).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.TotalPago).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Carritos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Carrito__IdMesa__76619304");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D5946642FA31CB0E");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdUsuarioLoginNavigation).WithMany(p => p.Clientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cliente__IdUsuar__69FBBC1F");
        });


        modelBuilder.Entity<ClienteFuncione>(entity =>
        {
            entity.HasKey(e => e.IdClienteFunciones).HasName("PK__ClienteF__260B7E2FF2B40507");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorSolicitada).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ClienteFunciones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClienteFu__IdCli__1B9317B3");
        });

        modelBuilder.Entity<DetalleCarrito>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCarrito).HasName("PK__DetalleC__27A5F83B49F01AEE");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.Precio).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCarritoNavigation).WithMany(p => p.DetalleCarritos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCa__IdCar__7755B73D");

            entity.HasOne(d => d.IdPlatoNavigation).WithMany(p => p.DetalleCarritos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCa__IdPla__7849DB76");
        });

        modelBuilder.Entity<DetalleCarritoPersonalizado>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCarritoPersonalizado).HasName("PK__DetalleC__F2C02F095C2E669F");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdDetalleCarritoNavigation).WithMany(p => p.DetalleCarritoPersonalizados)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCa__IdDet__793DFFAF");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleCarritoPersonalizados)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCa__IdPro__7A3223E8");
        });

        modelBuilder.Entity<DetalleMaestro>(entity =>
        {
            entity.HasKey(e => e.IdDetalleMaestro).HasName("PK__DetalleM__B96AADB68BD94CA9");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Campo1Decimal).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.Campo1Int).HasDefaultValueSql("((0))");
            entity.Property(e => e.Campo1Nvarchar).HasDefaultValueSql("('')");
            entity.Property(e => e.Campo2Decimal).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.Campo2Int).HasDefaultValueSql("((0))");
            entity.Property(e => e.Campo2Nvarchar).HasDefaultValueSql("('')");
            entity.Property(e => e.Campo3Decimal).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.Campo3Int).HasDefaultValueSql("((0))");
            entity.Property(e => e.Campo3Nvarchar).HasDefaultValueSql("('')");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCabezeraMaestroNavigation).WithMany(p => p.DetalleMaestros)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleMa__IdCab__681373AD");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.IdDetallePedido).HasName("PK__DetalleP__48AFFD95ECDB8FDB");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.Precio).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePe__IdPed__6DCC4D03");

            entity.HasOne(d => d.IdPlatoNavigation).WithMany(p => p.DetallePedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePe__IdPla__6EC0713C");
        });

        modelBuilder.Entity<DetallePedidoPersonalizado>(entity =>
        {
            entity.HasKey(e => e.IdDetallePedidoPersonalizado).HasName("PK__DetalleP__E8A5535C5B482BB3");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdDetallePedidoNavigation).WithMany(p => p.DetallePedidoPersonalizados)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePe__IdDet__6FB49575");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidoPersonalizados)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePe__IdPro__70A8B9AE");
        });

        modelBuilder.Entity<DetallePlato>(entity =>
        {
            entity.HasKey(e => e.IdDetallePlato).HasName("PK__DetalleP__48A617D02F2F3F43");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Cantidad).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.CantidadMaxima).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdPlatoNavigation).WithMany(p => p.DetallePlatos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePl__IdPla__6AEFE058");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePlatos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePl__IdPro__6BE40491");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9E5FABBC97");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Cargo).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.Sucursal).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdUsuarioLoginNavigation).WithMany(p => p.Empleados)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleados__IdUsu__690797E6");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.IdMesa).HasName("PK__Mesa__4D7E81B12DE35B62");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.EstadoMesa).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<MesaFuncione>(entity =>
        {
            entity.HasKey(e => e.IdMesaFunciones).HasName("PK__MesaFunc__DBBFB6D17D973807");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorSolicitada).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.MesaFunciones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MesaFunci__IdMes__7B264821");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedido__9D335DC361574973");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Descuento).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.TotalPago).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Pedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido__IdMesa__6CD828CA");
        });

        modelBuilder.Entity<Plato>(entity =>
        {
            entity.HasKey(e => e.IdPlato).HasName("PK__Plato__4C943920CD1A055B");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__098892100FD99CA8");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.Precio).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReservas).HasName("PK__Reservas__1549E307E2BD0BD2");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.IdCliente).HasDefaultValueSql("((0))");
            entity.Property(e => e.IdMesa).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reservas).HasConstraintName("FK__Reservas__IdClie__74794A92");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Reservas).HasConstraintName("FK__Reservas__IdMesa__756D6ECB");
        });

        modelBuilder.Entity<UsuarioLogin>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioLogin).HasName("PK__UsuarioL__9E973030AE2F844F");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVentas).HasName("PK__Ventas__FF40C89F2123EBAD");

            entity.Property(e => e.Accion).HasDefaultValueSql("((0))");
            entity.Property(e => e.Descuento).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.Estado).HasDefaultValueSql("((0))");
            entity.Property(e => e.FecModifica).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FecRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorModifica).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.HorRegistro).HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(108)))");
            entity.Property(e => e.IdCliente).HasDefaultValueSql("((0))");
            entity.Property(e => e.IdPedido).HasDefaultValueSql("((0))");
            entity.Property(e => e.TotalPago).HasDefaultValueSql("((0.000))");
            entity.Property(e => e.UsuModifica).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsuRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta).HasConstraintName("FK__Ventas__IdClient__72910220");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Venta).HasConstraintName("FK__Ventas__IdPedido__73852659");
        });

        // BUSCADORES
        modelBuilder.Entity<PlatosInner>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<PlatosDetalleInner>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<CarritoInner>(entity =>
        {
            entity.HasNoKey();
        });


        modelBuilder.Entity<DetalleCarritoInner>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<DetalleCarritoPersonalizadoInner>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<PedidoInner>(entity =>
        {
            entity.HasNoKey();
        });


        modelBuilder.Entity<DetallePedidoInner>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<DetallePedidoPersonalizadoInner>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<ProductoInner>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<FilterEstadisticas>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<RankingCategoria>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<RankingPlatos>(entity =>
        {
            entity.HasNoKey();
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
