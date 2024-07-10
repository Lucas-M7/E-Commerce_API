using System.Security.Claims;
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
public class UsersController(IUsuarioService usuarioService, ConnectContext context) : ControllerBase
{
    private readonly IUsuarioService _usuarioService = usuarioService;
    private readonly ConnectContext _context = context;

    #region Cadastro
    /// <summary>
    /// Create a user profile by entering your name, email address and password.
    /// </summary>
    /// <param name="userDTO"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("users")]
    public IActionResult CriarUsuario([FromBody] UsuarioDTO userDTO)
    {
        var validacaoUsuario = new UsuarioValidador(_context);
        var validacao = validacaoUsuario.UsuarioValidacao(userDTO);

        if (validacao.Mensagens.Count > 0)
            return BadRequest(validacao);

        var usuario = new UsuarioModel
        {
            Email = userDTO.Email,
            Nome = userDTO.Nome,
            Senha = userDTO.Senha
        };

        _usuarioService.Adicionar(usuario);

        return Created($"/user/{usuario.ID}", new UsuarioModelView
        {
            ID = usuario.ID,
            Email = usuario.Email,
            Nome = usuario.Nome
        });
    }
    #endregion

    #region Login
    /// <summary>
    /// Log in with your email address and password.
    /// </summary>
    /// <param name="loginDTO"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("users/login")]
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
            return Unauthorized("Erro ao fazer login, verifique o email e senha, caso não possua uma conta, crie uma.");
    }
    #endregion

    #region Listar 
    /// <summary>
    /// List existing users.
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("users")]
    public IActionResult ListarUsuarios([FromQuery] int? page)
    {
        var usuario = new List<UsuarioModelView>();
        var usuarios = _usuarioService.ListarUsuarios(page);

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
    /// Delete the profile by entering the id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("users/")]
    public IActionResult ApagarUsuario([FromQuery] int id)
    {
        if (_context.Usuarios.Any(x => x.ID == id))
        {
            _usuarioService.Apagar(id);
            return Ok("Usuário removido com sucesso.");
        }
        return NotFound("Usuário não encontrado, verifique o ID.");
    }
    #endregion
}