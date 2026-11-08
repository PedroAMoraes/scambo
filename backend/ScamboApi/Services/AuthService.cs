using System;
using System.Data.Common;
using System.Security.Authentication;
using ScamboApi.DTOs;
using ScamboApi.Models;

namespace ScamboApi.Services;

public class AuthService : IAuthService
{
    private readonly ScamboContext _db;

    public AuthService(ScamboContext db)
    {
        _db = db;
    }

    public string AutenticarUsuario(UsuarioLoginDTO usuario)
    {
        string senhaUsuario = _db.Usuarios.Where(x => x.Email == usuario.Email).FirstOrDefault().Senha;
        if(PasswordHasher.VerifyPassword(usuario.Senha, senhaUsuario)){
            string token = TokenService.GenerateToken(usuario);
            return token;
        }
        throw new Exception("Usuário ou senha incorretos");
    }

    public string CadastrarUsuario(UsuarioRegistroDTO usuario)
    {
        Usuario novoUsuario = new Usuario(usuario);
        HashPassword(novoUsuario);
        if(_db.Usuarios.Any(u => u.Email == usuario.Email)) throw new Exception("E-mail já cadastrado");
        _db.Usuarios.Add(novoUsuario);
        _db.SaveChanges();
        return AutenticarUsuario(new UsuarioLoginDTO{Email = usuario.Email, Senha = usuario.Senha});
    }
    private void HashPassword(Usuario usuario)=>
        usuario.Senha = PasswordHasher.HashPassword(usuario.Senha);
    
}
