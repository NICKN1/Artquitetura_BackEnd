using Curso_Arquitetura_Backend.Models.Usuarios;

namespace Curso_Arquitetura_Backend.Configurations
{
    public interface IAuthenticationService
    {
        string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput);
    }
}
