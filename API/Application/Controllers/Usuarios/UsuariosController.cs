using API.Domain.DTOs;
using API.Domain.Interfaces;
using API.Domain.Models;
using API.Domain.ModelViews;
using API.Infrastucture.DB;
using API.Services.Token;
using API.Services.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controller;

[ApiController]
[Route("api/")]
public class UsuariosController(IUsuarioService usuarioService, ConnectContext context) : ControllerBase
{
    private readonly IUsuarioService _usuarioService = usuarioService;
    private readonly ConnectContext _context = context;

    #region Cadastro
    /// <summary>
    /// Crie o seu perfil de usuário informando o nome, email e senha.
    /// </summary>
    /// <param name="usuarioDTO"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Faça o login aqui, informando o email e senha.
    /// </summary>
    /// <param name="loginDTO"></param>
    /// <returns></returns>
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

    #region Listar 
    /// <summary>
    /// Liste o usuários que existem.
    /// </summary>
    /// <param name="pagina"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("usuarios/")]
    public IActionResult ListarUsuarios(int? pagina)
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
    #endregion

    #region Apagar
    /// <summary>
    /// Apague o seu perfil informando o seu id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("usuarios/{id}")]
    public IActionResult ApagarUsuario(int id)
    {
        try
        {
            _usuarioService.Apagar(id);
            return Ok("Usuario removido com sucesso.");
        }
        catch
        {
            return BadRequest("Erro ao remover o usuário.");
        }
    }
    #endregion
}