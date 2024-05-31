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
    /// Criar perfil de usuário informando o nome, email e senha.
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
            return BadRequest(validacao);

        var usuario = new UsuarioModel
        {
            Email = usuarioDTO.Email,
            Nome = usuarioDTO.Nome,
            Senha = usuarioDTO.Senha
        };

        _usuarioService.Adicionar(usuario);

        return Created($"/usuario/{usuario.ID}", new UsuarioModelView
        {
            ID = usuario.ID,
            Email = usuario.Email,
            Nome = usuario.Nome
        });
    }
    #endregion

    #region Login
    /// <summary>
    /// Fazer login informando o email e senha.
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
            return Unauthorized("Erro ao fazer login, verifique o email e senha, caso não possua uma conta, crie uma.");
    }
    #endregion

    #region Listar 
    /// <summary>
    /// Listar usuários existentes.
    /// </summary>
    /// <param name="pagina"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("usuarios/")]
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
    #endregion

    #region Apagar
    /// <summary>
    /// Apagar o perfil informando o id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("usuarios/{id}")]
    public IActionResult ApagarUsuario([FromRoute] int id)
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