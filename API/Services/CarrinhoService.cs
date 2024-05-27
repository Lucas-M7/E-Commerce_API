using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infrastucture.DB;

namespace API.Services;

public class CarrinhoService(ConnectContext context) : ICarrinhoService
{
    private readonly ConnectContext _context = context;

    #region AdicionarAoCarrinho
    public void AdicionarAoCarrinho(string usuarioNome, int produtoId, int quantidade)
    {
        if (string.IsNullOrEmpty(usuarioNome) || produtoId <= 0 || quantidade <= 0)
            throw new BadHttpRequestException("Parâmetro inválidos.");

        try
        {
            var usuario = ObterUsuario(usuarioNome);
            var produto = ObterProduto(produtoId);

            var carrinhoProduto = _context.Carrinho
                .FirstOrDefault(c => c.UsuarioNome == usuarioNome && c.ProdutoID == produtoId);

            if (carrinhoProduto == null)
            {
                AdicionarNovoItem(usuarioNome, produto, quantidade);
                AtualizarPrecoTotalCarrinho(usuarioNome);
            }
            else
            {
                AtualizarItemExistente(carrinhoProduto, quantidade);
                AtualizarPrecoTotalCarrinho(usuarioNome);
            }

            _context.SaveChanges();
        }
        catch (FileNotFoundException ex)
        {
            throw new BadHttpRequestException(ex.Message);
        }
    }
    #endregion

    #region ListarItens
    public List<CarrinhoModel> ListarItensNoCarrinho(int? pagina = 1)
    {
        var consulta = _context.Carrinho.AsQueryable();

        int itensPorPagina = 10;

        if (pagina != null && pagina > 0)
            consulta = consulta.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);

        return [.. consulta];
    }
    #endregion

    #region RemoverItem
    public void RemoverDoCarrinho(int id, int quantidade)
    {
        if (id <= 0 || quantidade <= 0)
            throw new BadHttpRequestException("Parâmetro inválido.");

        var carrinhoItem = ObterCarrinho(id);

        if (carrinhoItem != null)
        {
            if (quantidade == carrinhoItem.Quantidade)
                _context.Carrinho.Remove(carrinhoItem);
            else if (quantidade > carrinhoItem.Quantidade)
                throw new BadHttpRequestException("Quantidade inválida.");
            else
            {
                carrinhoItem.Quantidade -= quantidade;
                carrinhoItem.Total = carrinhoItem.ProdutoPreco * carrinhoItem.Quantidade;
            }

            AtualizarPrecoTotalCarrinho(carrinhoItem.UsuarioNome);
            _context.SaveChanges();
        }
        else
            throw new FileNotFoundException("Carrinho não encontrado, verifique o ID");
    }
    #endregion

    #region ObterUsuario
    private UsuarioModel ObterUsuario(string usuarioNome)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Nome == usuarioNome)
            ?? throw new FileNotFoundException("Usuário não encontrado.");
    }
    #endregion

    #region ObterProduto
    protected ProdutoModel ObterProduto(int produtoId)
    {
        return _context.Produtos.FirstOrDefault(p => p.ProdutoID == produtoId)
            ?? throw new FileNotFoundException("Produto não encontrado.");
    }
    #endregion

    #region ObterCarrinho
    private CarrinhoModel ObterCarrinho(int carrinhoId)
    {
        return _context.Carrinho.FirstOrDefault(x => x.ID == carrinhoId)
            ?? throw new FileNotFoundException("Carrinho não encontrado.");
    }
    #endregion

    #region AdicionarNovoItem
    private void AdicionarNovoItem(string usuarioNome, ProdutoModel produto, int quantidade)
    {
        var totalCarrinho = _context.Carrinho
            .Where(c => c.UsuarioNome == usuarioNome)
            .Sum(c => c.ProdutoPreco * c.Quantidade);

        var carrinhoProduto = new CarrinhoModel
        {
            UsuarioNome = usuarioNome,
            ProdutoNome = produto.Nome,
            ProdutoPreco = produto.Preco,
            ProdutoID = produto.ProdutoID,
            Quantidade = quantidade,
            Total = produto.Preco * quantidade
        };

        AtualizarPrecoTotalCarrinho(usuarioNome);
        _context.Carrinho.Add(carrinhoProduto);
    }
    #endregion

    #region AtualizarItem
    private static void AtualizarItemExistente(CarrinhoModel carrinhoProduto, int quantidade)
    {
        carrinhoProduto.Quantidade += quantidade;
        carrinhoProduto.Total = carrinhoProduto.ProdutoPreco * carrinhoProduto.Quantidade;
    }
    #endregion

    #region AtualizarPrecoTotal
    private void AtualizarPrecoTotalCarrinho(string usuarioNome)
    {
        var carrinhoItens = _context.Carrinho
            .Where(c => c.UsuarioNome == usuarioNome).ToList();

        var totalCarrinho = carrinhoItens.Sum(c => c.Total);

        foreach (var item in carrinhoItens)
        {
            item.Total = totalCarrinho;
        }
    }
    #endregion
}