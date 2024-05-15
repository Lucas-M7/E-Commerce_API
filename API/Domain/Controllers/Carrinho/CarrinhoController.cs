using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infrastucture.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Domain.Controllers.Carrinho;

[Authorize]
[ApiController]
[Route("api")]
public class CarrinhoController(ConnectContext context, ICarrinhoService carrinhoService) : ControllerBase
{
    private readonly ConnectContext _context = context;
    private readonly ICarrinhoService _carrinhoService = carrinhoService;

    [HttpPost("carrinho")]
    public IActionResult AdicionarAoCarrinho(string usuarioNome, int produtoId, int quantidade)
    {
        try
        {
            _carrinhoService.AdicionarAoCarrinho(usuarioNome, produtoId, quantidade);
            return Ok("Item adicionado ao carrinho com sucesso.");
        }
        catch
        {
            return BadRequest("Erro ao adicionar o produto ao carrinho.");
        }
    }
}