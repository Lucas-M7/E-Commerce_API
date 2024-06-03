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
public class CarrinhoController(ICarrinhoService carrinhoService, ConnectContext context) : ControllerBase
{
    private readonly ICarrinhoService _carrinhoService = carrinhoService;
    private readonly ConnectContext _context = context;

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
        var validacaoCarrinho = new CarrinhoValidador(_context);
        var validacao = validacaoCarrinho.ValidacaoAdicionarAoCarrinho(usuarioNome, produtoId, quantidade);

        if (validacao.Mensagens.Count > 0)
            return BadRequest(validacao);

        _carrinhoService.AdicionarAoCarrinho(usuarioNome, produtoId, quantidade);

        return Ok("Produto adicionado ao carrinho com sucesso.");
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

        var carrinhoValidacao = new CarrinhoValidador(_context);
        var validacao = carrinhoValidacao.ValidarRemoverDoCarrinho(carrinhoId, quantidade);

        if (validacao.Mensagens.Count > 0)
            return BadRequest(validacao);

        _carrinhoService.RemoverDoCarrinho(carrinhoId, quantidade);

        return Ok("Produto removido do carrinho com sucesso");
    }
}