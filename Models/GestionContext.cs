using Microsoft.EntityFrameworkCore;

namespace AWGestion.Models
{
    public class GestionContext: DbContext
    {
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<CatArticulo> CatArticulos { get; set; }
        public DbSet<CatTipoArt> CatTipoArts { get; set; }
        public DbSet<InvArticulo> InvArticulos { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<AspNetRole> AspNetRole { get; set; }
        public DbSet<AspNetUserRole>AspNetUserRole { get; set; }
        public DbSet<Salida> Salidas { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public GestionContext(DbContextOptions<GestionContext> options) : base(options)
        {

        }
    }
}