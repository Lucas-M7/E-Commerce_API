using API.Domain.Interfaces;
using API.Domain.ModelViews;
using API.Infrastucture.DB;
using API.Services.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Carrinho;

[Authorize]
[ApiController]
[Route("api")]
public class CartController(ICarrinhoService carrinhoService, ConnectContext context) : ControllerBase
{
    private readonly ICarrinhoService _carrinhoService = carrinhoService;
    private readonly ConnectContext _context = context;

    /// <summary>
    /// Add desired product to cart
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="productId"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    [HttpPost("cart/")]
    public IActionResult AdicionarAoCarrinho([FromQuery] string userName, int productId, int quantity)
    {
        var validacaoCarrinho = new CarrinhoValidador(_context);
        var validacao = validacaoCarrinho.ValidacaoAdicionarAoCarrinho(userName, productId, quantity);

        if (validacao.Mensagens.Count > 0)
            return BadRequest(validacao);

        _carrinhoService.AdicionarAoCarrinho(userName, productId, quantity);

        return Ok("Produto adicionado ao carrinho com sucesso.");
    }

    /// <summary>
    /// Return a list of 10 products in cart
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet("cart")]
    public IActionResult ListarItensNoCarrinho([FromQuery] int? page)
    {
        try
        {
            var itens = _carrinhoService.ListarItensNoCarrinho(page)
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
    /// Deletes a product in the cart according to the desired quantity
    /// </summary>
    /// <param name="cartId"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    [HttpDelete("cart/")]
    public IActionResult RemoverItemDoCarrinho([FromQuery] int cartId, int quantity)
    {

        var carrinhoValidacao = new CarrinhoValidador(_context);
        var validacao = carrinhoValidacao.ValidarRemoverDoCarrinho(cartId, quantity);

        if (validacao.Mensagens.Count > 0)
            return BadRequest(validacao);

        _carrinhoService.RemoverDoCarrinho(cartId, quantity);

        return Ok("Produto removido do carrinho com sucesso");
    }
}