using API.Domain.Interfaces;
using API.Domain.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Produtos;

[ApiController]
[Route("api/")]
public class ProductsController(IProdutoService produtoService) : ControllerBase
{
    private readonly IProdutoService _produtoService = produtoService;

    /// <summary>
    /// Lists all available products.
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet("products")]
    public IActionResult ListarProdutos([FromQuery] int? page)
    {
        var produtos = _produtoService.ListarProdutos(page)
                .Select(item => new ProdutoModelView
                {
                    ID = item.ProdutoID,
                    Nome = item.Nome,
                    Descricao = item.Descricao,
                    Preco = item.Preco,
                    Categoria = item.Categoria,
                    Estoque = item.Estoque
                })
                .ToList();

        return Ok(produtos);
    }
}
