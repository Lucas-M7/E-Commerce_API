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

    /// <summary>
    /// Adiciona o produto desejado ao carrinho.
    /// </summary>
    /// <param name="usuarioNome"></param>
    /// <param name="produtoId"></param>
    /// <param name="quantidade"></param>
    /// <returns></returns>
    [HttpPost("carrinho/{usuarioNome}/{produtoId}/{quantidade}")]
    public IActionResult AdicionarAoCarrinho([FromRoute] string usuarioNome, int produtoId, int quantidade)
    {
        try
        {
            _carrinhoService.AdicionarAoCarrinho(usuarioNome, produtoId, quantidade);
            return Ok("Item adicionado ao carrinho com sucesso.");
        }
        catch
        {
            return BadRequest("Erro ao adicionar o produto ao carrinho, verifique o id ou o nome de usuário.");
        }
    }

    /// <summary>
    /// Retorna uma lista de até 10 produtos diferentes que estão no carrinho.
    /// </summary>
    /// <param name="pagina"></param>
    /// <returns></returns>
    [HttpGet("carrinho")]
    public IActionResult ListarItensNoCarrinho([FromQuery] int? pagina)
    {
        try
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
        catch
        {
            return BadRequest("Erro ao listar os itens do carrinho.");
        }
    }

    /// <summary>
    /// Deleta um produto que está no carrinho de acordo com a quantidade desejada.
    /// </summary>
    /// <param name="carrinhoId"></param>
    /// <param name="quantidade"></param>
    /// <returns></returns>
    [HttpDelete("carrinho/{carrinhoId}/{quantidade}")]
    public IActionResult RemoverItemDoCarrinho([FromRoute] int carrinhoId, int quantidade)
    {
        try
        {
            _carrinhoService.RemoverDoCarrinho(carrinhoId, quantidade);
            return Ok("Produto removido com sucesso.");
        }
        catch
        {
            return BadRequest("Erro ao remover produto do carrinho, verifique o ID ou a quantidade.");
        }
    }
}