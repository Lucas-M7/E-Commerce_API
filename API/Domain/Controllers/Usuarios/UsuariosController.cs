using API.Domain.DTOs;
using API.Domain.Interfaces;
using API.Domain.Models;
using API.Domain.ModelViews;
using API.Domain.Token;
using API.Domain.Validations;
using API.Infrastucture.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Domain.Controller;

[ApiController]
[Route("api/")]
public class UsuariosController(IUsuarioService usuarioService, ConnectContext context) : ControllerBase
{
    private readonly IUsuarioService _usuarioService = usuarioService;
    private readonly ConnectContext _context = context;

    #region Cadastro
    [AllowAnonymous]
    [HttpPost("usuarios")]
    public IActionResult CriarUsuario([FromBody] UsuarioDTO usuarioDTO)
    {
        var validacaoUsuario = new UsuarioValidador(_context);
        var validacao = validacaoUsuario.UsuarioValidacao(usuarioDTO);

        if (validacao.Mensagens.Count > 0)
            return BadRequest(new ErrorValidacao {
                Mensagens = validacao.Mensagens
            });

        var usuario = new UsuarioModel
        {
            Email = usuarioDTO.Email,
            Nome = usuarioDTO.Nome,
            Senha = usuarioDTO.Senha
        };

        _usuarioService.Adicionar(usuario);

        return Ok();
    }
    #endregion

    #region Login
    [AllowAnonymous]
    [HttpPost("usuarios/login")]
    public IActionResult FazerLogin([FromBody] LoginDTO loginDTO)
    {
        var usuario = _usuarioService.Login(loginDTO);

        if (usuario != null)
        {
            string token = TokenGerador.GerarTokenJwT(usuario);

            return Ok(new UsuarioLogado
            {
                Email = usuario.Email,
                Nome = usuario.Nome,
                Token = token
            });
        }
        else
        {
            return Unauthorized();
        }
    }
    #endregion

    [Authorize]
    [HttpGet("usuarios")]
    public IActionResult ListarUsuarios([FromQuery] int? pagina)
    {
        var usuario = new List<UsuarioModelView>();
        var usuarios = _usuarioService.ListarUsuarios(pagina);

        foreach (var item in usuarios)
        {
            usuario.Add(new UsuarioModelView
            {
                ID = item.ID,
                Email = item.Email,
                Nome = item.Nome
            });
        }

        return Ok(usuario);
    }
}