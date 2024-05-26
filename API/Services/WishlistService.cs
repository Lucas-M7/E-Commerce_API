using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infrastucture.DB;

namespace API.Services;

public class WishlistService(ConnectContext context) : IWishlistService
{
    private readonly ConnectContext _context = context;

    public void AdicionarProdutoNaLista(int produtoId)
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

    private WishlistModel ObterIdDaLista(int id)
    {
        return _context.Wishlist.FirstOrDefault(x => x.Id == id)
            ?? throw new FileNotFoundException("Id não encontrado.");
    }

    public List<WishlistModel> ListarItensDesejados(int? pagina = 1)
    {
        var consulta = _context.Wishlist.AsQueryable();

        int itensPorPagina = 10;

        if (pagina != null && pagina > 0)
            consulta = consulta.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);

        return [.. consulta];
    }

    public void RemoverProdutoDaLista(int listaId)
    {
        if (listaId <= 0)
            throw new BadHttpRequestException("Parâmetro inválido.");

        var idDaLista = ObterIdDaLista(listaId);

        _context.Remove(idDaLista);
        _context.SaveChanges();
    }
}