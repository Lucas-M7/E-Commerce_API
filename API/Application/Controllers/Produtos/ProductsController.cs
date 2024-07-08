using API.Domain.Interfaces;
using API.Domain.ModelViews;
using API.Infrastucture.DB;
using API.Services.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Produtos;

[ApiController]
[Route("api/")]
public class ProductsController(IProdutoService produtoService, ConnectContext context) : ControllerBase
{
    private readonly IProdutoService _produtoService = produtoService;
    private readonly ConnectContext _context = context;

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

    /// <summary>
    /// List products for category.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet("products/category")]
    public IActionResult ListarProdutosPelaCategoria([FromQuery] string category, int? page)
    {
        var validacaoProdutos = new ProdutosValidador(_context);
        var validacao = validacaoProdutos.ValidacaoBuscarCategoria(category);

        if (validacao.Mensagens.Count > 0)
            return BadRequest(validacao);

        var produtos = _produtoService.BuscarProdutoPelaCategoria(category, page)
            .Select(item => new ProdutoModelView
            {
                ID = item.ProdutoID,
                Nome = item.Nome,
                Descricao = item.Descricao,
                Preco = item.Preco,
                Categoria = item.Categoria,
                Estoque = item.Estoque
            }).ToList();

        return Ok(produtos);
    }

    /// <summary>
    /// List products for price.
    /// </summary>
    /// <param name="price"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet("products/price")]
    public IActionResult ListarProdutosPorPreco([FromQuery] double price, int? page)
    {
        var validacaoProdutos = new ProdutosValidador(_context);
        var validacao = validacaoProdutos.ValidacaoBuscarPorPreco(price);

        if (validacao.Mensagens.Count > 0)
            return BadRequest(validacao);

        var produtos = _produtoService.BuscarProdutoPeloPreco(price, page)
            .Select(i => new ProdutoModelView
            {
                ID = i.ProdutoID,
                Nome = i.Nome,
                Descricao = i.Descricao,
                Preco = i.Preco,
                Categoria = i.Categoria,
                Estoque = i.Estoque
            }).ToList();

        return Ok(produtos);
    }

    /// <summary>
    /// List products for name.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet("products/name")]
    public IActionResult ListarProdutosPeloNome([FromQuery] string name, int? page)
    {
        var validacaoProdutos = new ProdutosValidador(_context);
        var validacao = validacaoProdutos.ValidacaoBuscarPeloNome(name);

        if (validacao.Mensagens.Count > 0)
            return BadRequest(validacao);

        var produtos = _produtoService.BuscarProdutoPeloNome(name, page)
            .Select(i => new ProdutoModelView
            {
                ID = i.ProdutoID,
                Nome = i.Nome,
                Descricao = i.Descricao,
                Preco = i.Preco,
                Categoria = i.Categoria,
                Estoque = i.Estoque
            }).ToList();

        return Ok(produtos);
    }
}
