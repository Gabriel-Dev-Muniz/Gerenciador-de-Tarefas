using GerenciadorDeTarefa.DTOs;
using GerenciadorDeTarefa.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly UsuarioService _usuarioService;
    private readonly TokenService _tokenService;

    public AuthController(
        UsuarioService usuarioService,
        TokenService tokenService)
    {
        _usuarioService = usuarioService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UsuarioCreateDto dto)
    {
        var usuario = _usuarioService.Criar(dto);

        return Ok(new
        {
            usuario.Id,
            usuario.Nome,
            usuario.Email
        });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var usuario = _usuarioService.Autenticar(dto.Email, dto.Senha);

        if (usuario == null)
            return Unauthorized("Email ou senha inválidos");

        var token = _tokenService.GerarToken(usuario);

        return Ok(new
        {
            token,
            nome = usuario.Nome
        });
    }
}
