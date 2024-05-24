using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infrastucture.DB;

namespace API.Services;

public class WishlistService(ConnectContext context) : IWishlistService
{
    private readonly ConnectContext _context = context;

    public void AdicionarNaLista(int produtoId)
    {
        if (produtoId <= 0)
            throw new BadHttpRequestException("Parâmetro inválidos.");

        var produto = ObterProduto(produtoId);
        var listaProduto = _context.Wishlist
            .FirstOrDefault(x => x.ProdutoId == produtoId);

        if (listaProduto == null)
            AdicionarNovoItem(produto);

        _context.SaveChanges();        
    }

    private void AdicionarNovoItem(ProdutoModel produto)
    {
        var desejoProduto = new WishlistModel
        {
            ProdutoNome = produto.Nome,
            ProdutoPreco = produto.Preco,
            ProdutoId = produto.ProdutoID,
        };

        _context.Wishlist.Add(desejoProduto);
    }

    private ProdutoModel ObterProduto(int produtoId)
    {
        return _context.Produtos.FirstOrDefault(p => p.ProdutoID == produtoId)
            ?? throw new FileNotFoundException("Produto não encontrado.");
    }

    public List<WishlistModel> ListaDeDesejo(int? pagina = 1)
    {
        var consulta = _context.Wishlist.AsQueryable();

        int itensPorPagina = 10;

        if (pagina != null && pagina > 0)
            consulta = consulta.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);

        return [.. consulta];
    }

    public void RemoverDaLista(int produtoId)
    {
        throw new NotImplementedException();
    }
}