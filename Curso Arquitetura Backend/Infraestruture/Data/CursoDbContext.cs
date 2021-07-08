using Curso_Arquitetura_Backend.Business.Entites;
using Curso_Arquitetura_Backend.Infraestruture.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Curso_Arquitetura_Backend.Infraestruture.Data
{
    public class CursoDbContext : DbContext
    {
        public CursoDbContext(DbContextOptions<CursoDbContext> options) :base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CursoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuario { get; set; }
    }
}
