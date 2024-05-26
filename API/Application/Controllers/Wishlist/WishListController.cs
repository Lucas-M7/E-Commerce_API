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

    [HttpPost("WishList/{produtoId}")]
    public IActionResult AdicionarNaLista(int produtoId)
    {
        _wishhList.AdicionarProdutoNaLista(produtoId);
        return Ok("Produto adicionado a lista de desejo.");
    }

    [HttpGet("WishList")]
    public IActionResult ListarItensDesejados(int? pagina)
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

    [HttpDelete("WishList/{listaId}")]
    public IActionResult RemoverItenDaListaDeDesejo(int listaId)
    {
        _wishhList.RemoverProdutoDaLista(listaId);
        return Ok("Produto removido da lista de desejo.");
    }
}