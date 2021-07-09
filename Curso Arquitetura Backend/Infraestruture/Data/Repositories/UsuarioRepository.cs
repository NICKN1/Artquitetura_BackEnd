using Curso_Arquitetura_Backend.Business.Entites;
using Curso_Arquitetura_Backend.Business.Repositories;

namespace Curso_Arquitetura_Backend.Infraestruture.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CursoDbContext _contexto;

        public UsuarioRepository(CursoDbContext contexto)
        {
            _contexto = contexto;
        }

        public void Adicionar(Usuario usuario)
        {
            _contexto.Usuario.Add(usuario);
            
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }
    }
}
