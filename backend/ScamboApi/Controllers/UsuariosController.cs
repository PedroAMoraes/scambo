using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScamboApi.DTOs;
using ScamboApi.Models;
using ScamboApi.Services;
namespace ScamboApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly ScamboContext _db;
    private readonly IAuthService _auth;
    public UsuariosController(ScamboContext db, IAuthService auth)
    {
        _db = db;
        _auth = auth;
    }
    [HttpGet]
    public IActionResult ObterTodos()
    {
        return Ok("Lista de usuários cadastrados");
    }
    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        return Ok($"Usuário com ID {id} encontrado");
    }
  
    [HttpPost]
    public IActionResult CriarUsuario([FromBody] UsuarioRegistroDTO novoUsuario)
    {
       _auth.CadastrarUsuario(novoUsuario);
        return Created("", novoUsuario);
    }
    [HttpPut("{id}")]
    public IActionResult AtualizarUsuario(int id, [FromBody] Usuario usuarioAtualizado)
    {
        return Ok($"Usuário com ID {id} atualizado");
    }
    [HttpDelete("{id}")]
    public IActionResult ExcluirUsuario(int id)
    {
        return NoContent();
    }
    [Authorize]
    [HttpGet("eu")]
    public IActionResult MeuUsuario()
    {
        var user = User.Claims.Where(x => x.Type ==ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
        return Ok(user);
    }
}
