using API.Domain.Interfaces;
using API.Domain.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Wishlist;

[Authorize]
[ApiController]
[Route("api/")]
public class WishListController(IWishlistService wishlist) : ControllerBase
{
    private readonly IWishlistService _wishhList = wishlist;

    /// <summary>
    /// Adicionar o produto desejado na lista de desejos.
    /// </summary>
    /// <param name="produtoId"></param>
    /// <returns></returns>
    [HttpPost("wishlist/{produtoId}")]
    public IActionResult AdicionarNaLista([FromRoute] int produtoId)
    {
        _wishhList.AdicionarProdutoNaLista(produtoId);
        return Ok("Produto adicionado a lista de desejo.");
    }

    /// <summary>
    /// Lista itens que estão na lista de desejo.
    /// </summary>
    /// <param name="pagina"></param>
    /// <returns></returns>
    [HttpGet("wishlist/")]
    public IActionResult ListarItensDesejados([FromQuery] int? pagina)
    {
        var itens = _wishhList.ListarItensDesejados(pagina)
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
    /// Remove o item da lista de desejo.
    /// </summary>
    /// <param name="listaId"></param>
    /// <returns></returns>
    [HttpDelete("wishlist/{listaId}")]
    public IActionResult RemoverItenDaListaDeDesejo([FromRoute] int listaId)
    {
        _wishhList.RemoverProdutoDaLista(listaId);
        return Ok("Produto removido da lista de desejo.");
    }
}