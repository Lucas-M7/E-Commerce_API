using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infrastucture.DB;
using API.Services.Validations;

namespace API.Services;

public class WishlistService(ConnectContext context, WishlistValidador wishlistValidador) : IWishlistService
{
    private readonly ConnectContext _context = context;
    private readonly WishlistValidador _wishlistValidador = wishlistValidador;

    public void AdicionarProdutoNaLista(int produtoId)
    {
        var validacao = _wishlistValidador.ValidacaoAdicionarALista(produtoId);
        if (validacao.Mensagens.Count != 0)
            throw new BadHttpRequestException(string.Join("; ", validacao.Mensagens));

        var produto = ObterProduto(produtoId);
        var listaProduto = _context.Wishlist
            .FirstOrDefault(x => x.ProdutoId == produtoId);

        if (listaProduto == null)
            AdicionarNovoItem(produto);

        _context.SaveChanges();
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
        var validacao = _wishlistValidador.ValidacaoRemoverDaLista(listaId);
        if (validacao.Mensagens.Count != 0)
            throw new BadHttpRequestException(string.Join("; ", validacao.Mensagens));

        var idDaLista = ObterIdDaLista(listaId);

        _context.Remove(idDaLista);
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

    protected ProdutoModel ObterProduto(int produtoId)
    {
        return _context.Produtos.FirstOrDefault(p => p.ProdutoID == produtoId)
            ?? throw new FileNotFoundException("Produto não encontrado.");
    }

    private WishlistModel ObterIdDaLista(int id)
    {
        return _context.Wishlist.FirstOrDefault(x => x.Id == id)
            ?? throw new FileNotFoundException("Id não encontrado.");
    }
}