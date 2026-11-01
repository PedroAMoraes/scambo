using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ScamboApi.DTOs;

namespace ScamboApi.Services;

public interface IAuthService
{
    string CadastrarUsuario(UsuarioRegistroDTO usuario);
    string AutenticarUsuario(UsuarioLoginDTO usuario);
}
