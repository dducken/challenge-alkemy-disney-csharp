using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace Data
{
    public class Context : DbContext
    {
        private string cadenaConexion;

        public DbSet<Personaje> Personajes {get; set;} 
        public DbSet<Pelicula> Peliculas {get; set;}
        public DbSet<Genero> Generos {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Rol> Roles {get; set;}


        public Context(DbContextOptions options) : base(options)
        {
        }

        public Context() : base()
        {
            var configuration = GetConfiguration();
            cadenaConexion = configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(cadenaConexion);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);

        }

    }
}