using API.Domain.Interfaces;
using API.Domain.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Produtos;

[Authorize]
[ApiController]
[Route("api/")]
public class ProdutosController(IProdutoService produtoService) : ControllerBase
{
    private readonly IProdutoService _produtoService = produtoService;

    [HttpGet("produtos")]
    public IActionResult ListarProdutos([FromQuery] int? pagina)
    {
        var produtos = _produtoService.ListarProdutos(pagina)
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
