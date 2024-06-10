using API.Domain.Interfaces;
using API.Domain.ModelViews;
using API.Infrastucture.DB;
using API.Services.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Wishlist;

[Authorize]
[ApiController]
[Route("api/")]
public class WishListController(IWishlistService wishlist, ConnectContext context) : ControllerBase
{
    private readonly IWishlistService _wishhList = wishlist;
    private readonly ConnectContext _context = context;

    /// <summary>
    /// Add the desired product to your wish list.
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    [HttpPost("wishlist/")]
    public IActionResult AdicionarNaLista([FromQuery] int productId)
    {
        var validacaoLista = new WishlistValidador(_context);
        var validacao = validacaoLista.ValidacaoAdicionarALista(productId);

        if (validacao.Mensagens.Count > 0)
            return BadRequest(validacao);

        _wishhList.AdicionarProdutoNaLista(productId);
        return Ok("Produto adicionado a lista de desejo.");
    }

    /// <summary>
    /// List items that are on your wish list.
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet("wishlist/")]
    public IActionResult ListarItensDesejados([FromQuery] int? page)
    {
        var itens = _wishhList.ListarItensDesejados(page)
        .Select(item => new WishListModelView
        {
            ListaId = item.Id,
            ProdutoId = item.ProdutoId,
            ProdutoNome = item.ProdutoNome,
            ProdutoPreco = item.ProdutoPreco
        }).ToList();

        return Ok(itens);
    }

    /// <summary>
    /// Removes the item from the wish list.
    /// </summary>
    /// <param name="listId"></param>
    /// <returns></returns>
    [HttpDelete("wishlist/")]
    public IActionResult RemoverItenDaListaDeDesejo([FromQuery] int listId)
    {
        var validacaoLista = new WishlistValidador(_context);
        var validacao = validacaoLista.ValidacaoRemoverDaLista(listId);

        if (validacao.Mensagens.Count > 0)
            return BadRequest(validacao);

        _wishhList.RemoverProdutoDaLista(listId);
        return Ok("Produto removido da lista de desejo.");
    }
}