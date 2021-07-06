using Curso_Arquitetura_Backend.Models;
using Curso_Arquitetura_Backend.Models.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso_Arquitetura_Backend.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModellInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos Obrigatórios", Type = typeof(ValidaCampoViewModelOutPut))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]

        [HttpPost]
        [Route("logar")]
        public IActionResult Logar(Models.Usuarios.LoginViewModellInput loginViewModellInput)
        {
            return Ok(loginViewModellInput);
        }

        [HttpPost]
        [Route("Registrar")]
        public IActionResult Registrar(Models.Usuarios.RegistroViewModellInput loginViewModellInput)
        {
            return Created("", loginViewModellInput);
        }
    }
}
