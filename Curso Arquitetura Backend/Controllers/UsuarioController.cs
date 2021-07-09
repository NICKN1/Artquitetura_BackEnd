using Curso_Arquitetura_Backend.Business.Entites;
using Curso_Arquitetura_Backend.Business.Repositories;
using Curso_Arquitetura_Backend.Filters;
using Curso_Arquitetura_Backend.Infraestruture.Data;
using Curso_Arquitetura_Backend.Infraestruture.Data.Repositories;
using Curso_Arquitetura_Backend.Models;
using Curso_Arquitetura_Backend.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        

        IUsuarioRepository _usuarioRepository;

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModellInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos Obrigatórios", Type = typeof(ValidaCampoViewModelOutPut))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]

        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(Models.Usuarios.LoginViewModellInput loginViewModellInput)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(new ValidaCampoViewModelOutPut(ModelState.SelectMany(sm => sm.Value.Errors).Select(s => s.ErrorMessage)));
            //}
            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = 1,
                Login = "Junior",
                Email = "Junior.Arrais@gmail.com"
            };

            var secret = Encoding.ASCII.GetBytes("MzfsT&d9gprP>!9$Es(X!5g@;ef!5sbk:jH\\2.8ZP'qY#7");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuarioViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, usuarioViewModelOutput.Email.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

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
