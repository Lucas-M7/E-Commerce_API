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
    public IActionResult AdicionarAoCarrinho(int usuarioId, int produtoId, int quantidade)
    {
        try
        {
            var usuarioI = _context.Usuarios.FirstOrDefault(u => u.ID == usuarioId);

            if (usuarioI == null)
                return NotFound("Usuário não encontrado.");

            var produtoI = _context.Produtos.FirstOrDefault(p => p.ProdutoID == produtoId);

            if (produtoI == null)
                return NotFound("Produto não encontrado.");

            var cartItem = _context.Carrinho.FirstOrDefault(c => c.UsuarioID == usuarioId && c.ProdutoID == produtoId);

            if (cartItem == null)
            {
                cartItem = new CarrinhoModel
                {
                    UsuarioID = usuarioId,
                    ProdutoID = produtoId,
                    UsuarioNome = usuarioI.Nome,
                    ProdutoNome = produtoI.Nome,
                    ProdutoPreco = produtoI.Preco,
                    Quantidade = quantidade
                };
                _context.Carrinho.Add(cartItem);
            }
            else
                cartItem.Quantidade += quantidade;

            _context.SaveChanges();
            return Ok("Item adicionado ao carrinho com sucesso.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao adicionar o produto ao carrinho.: {ex.Message}");
        }
    }
}