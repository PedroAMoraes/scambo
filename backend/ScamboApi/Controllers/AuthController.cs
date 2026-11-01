using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScamboApi.DTOs;
using ScamboApi.Services;

namespace ScamboApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }
        [HttpPost("login")]
        public IActionResult AutenticarUsuario(UsuarioLoginDTO usuario)
        {
            try{
            string authToken = _auth.AutenticarUsuario(usuario);

                return Ok(new {token = authToken});
            }
            catch(Exception e)
            {
                return Unauthorized(new {message = e.Message});
            }
        }
        [HttpPost("cadastro")]
        public IActionResult CriarUsuario([FromBody] UsuarioRegistroDTO novoUsuario)
        {
            _auth.CadastrarUsuario(novoUsuario);
            return Created("", novoUsuario);
        }
    }
}
