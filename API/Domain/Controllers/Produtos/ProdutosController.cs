using API.Domain.Interfaces;
using API.Domain.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Domain.Controllers.Produtos;

[ApiController]
[Route("api/")]
public class ProdutosController(IProdutoService produtoService) : ControllerBase
{
    private readonly IProdutoService _produtoService = produtoService;

    [Authorize]
    [HttpGet("produtos")]
    public IActionResult ListarProdutos([FromQuery] int? pagina)
    {
        var produto = new List<ProdutoModelView>();
        var produtos = _produtoService.ListarProdutos(pagina);

        foreach(var item in produtos)
        {
            produto.Add(new ProdutoModelView
            {
                ID = item.ID,
                Nome = item.Nome,
                Descricao = item.Descricao,
                Preco = item.Preco,
                Categoria = item.Categoria,
                Estoque = item.Estoque
            });
        }

        return Ok(produto);
    }
}
