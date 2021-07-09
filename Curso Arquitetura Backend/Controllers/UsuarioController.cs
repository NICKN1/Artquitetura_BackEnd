using Curso_Arquitetura_Backend.Business.Entites;
using Curso_Arquitetura_Backend.Business.Repositories;
using Curso_Arquitetura_Backend.Configurations;
using Curso_Arquitetura_Backend.Filters;
using Curso_Arquitetura_Backend.Infraestruture.Data;
using Curso_Arquitetura_Backend.Infraestruture.Data.Repositories;
using Curso_Arquitetura_Backend.Models;
using Curso_Arquitetura_Backend.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Curso_Arquitetura_Backend.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationService _authenticationService;
        public UsuarioController(IUsuarioRepository usuarioRepository, IAuthenticationService authenticationService)
        {
            _usuarioRepository = usuarioRepository;  
            _authenticationService = authenticationService;
        }

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModellInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos Obrigatórios", Type = typeof(ValidaCampoViewModelOutPut))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]

        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(Models.Usuarios.LoginViewModellInput loginViewModellInput)
        {
            var usuario = _usuarioRepository.ObterUsuario(loginViewModellInput.Login);

            if (usuario == null)
            {
                return BadRequest("Houve um erro ao tentar acessar.");
            }

            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = usuario.Codigo,
                Login = loginViewModellInput.Login,
                Email = usuario.Email
            }; 
           
            var token = _authenticationService.GerarToken(usuarioViewModelOutput);

            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }
        //Summary
        // Este serviço permite cadastrar usuários no Banco de Dados
        //Summary
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModellInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos Obrigatórios", Type = typeof(ValidaCampoViewModelOutPut))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("Registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistroViewModellInput loginViewModellInput)
        {
            /*
            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            optionsBuilder.UseSqlServer("Server=localhostjuniorarrais.database.windows.net;Database=Curso;user=juniorarrais;password=arrais8936#");

            CursoDbContext contexto = new CursoDbContext(optionsBuilder.Options);

            var migracoesPedentes = contexto.Database.GetPendingMigrations();
            if (migracoesPedentes.Count() > 0)
            {
                contexto.Database.Migrate();
            }
            */
            var usuario = new Usuario();
            usuario.Login = loginViewModellInput.Login;
            usuario.Email = loginViewModellInput.Email;
            usuario.Senha = loginViewModellInput.Senha;
            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();

            


            return Created("", loginViewModellInput);
        }
    }
}
