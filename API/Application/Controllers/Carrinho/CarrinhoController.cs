using API.Domain.Interfaces;
using API.Domain.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Carrinho;

[Authorize]
[ApiController]
[Route("api")]
public class CarrinhoController(ICarrinhoService carrinhoService) : ControllerBase
{
    private readonly ICarrinhoService _carrinhoService = carrinhoService;

    [HttpPost("carrinho/{usuarioNome}/{produtoId}/{quantidade}")]
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

    [HttpGet("carrinho/{pagina}")]
    public IActionResult ListarItensNoCarrinho(int? pagina)
    {
        var itens = _carrinhoService.ListarItensNoCarrinho(pagina)
            .Select(item => new ProdutoCarrinhoModelView
            {
                ID = item.ID,
                UsuarioNome = item.UsuarioNome,
                ProdutoNome = item.ProdutoNome,
                ProdutoPreco = item.ProdutoPreco,
                ProdutoQuantidade = item.Quantidade,
                ValorDoCarrinho = item.Total
            }).ToList();

        return Ok(itens);
    }

    [HttpDelete("carrinho/{carrinhoId}/{quantidade}")]
    public IActionResult RemoverItemDoCarrinho(int carrinhoId, int quantidade)
    {
        try
        {
            _carrinhoService.RemoverDoCarrinho(carrinhoId, quantidade);
            return Ok("Produto removido com sucesso.");
        }
        catch
        {
            return BadRequest("Erro ao remover produto do carrinho.");
        }
    }
}