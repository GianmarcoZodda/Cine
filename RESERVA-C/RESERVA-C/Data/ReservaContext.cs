using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RESERVA_C.Models;

namespace RESERVA_C.Data
{
    public class ReservaContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public ReservaContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser<int>>().ToTable("Personas");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("PersonasRoles");
        }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Funcion> Funciones { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<TipoSala> TipoSalas { get; set; }

        public DbSet<Rol> Roles { get; set; }
    }
}
