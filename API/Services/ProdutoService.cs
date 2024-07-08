using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infrastucture.DB;

namespace API.Services;

public class ProdutoService(ConnectContext context) : IProdutoService
{
    private readonly ConnectContext _context = context;

    readonly int itensPorPagina = 20;

    public List<ProdutoModel> BuscarProdutoPeloNome(string nome, int? pagina)
    {
        var consulta = _context.Produtos
            .Where(p => p.Nome.Contains(nome))
            .AsQueryable();

        if (pagina != null) 
            consulta = consulta.Skip((int)pagina - 1 * itensPorPagina).Take(itensPorPagina);

        return [.. consulta];      
    }

    public List<ProdutoModel> BuscarProdutoPelaCategoria(string categoria, int? pagina)
    {
        var consulta = _context.Produtos
            .Where(p => p.Categoria == categoria)
            .AsQueryable();

        if (pagina != null)
        {
            consulta = consulta.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
        }

        return [.. consulta];
    }

    public List<ProdutoModel> BuscarProdutoPeloPreco(double preco, int? pagina)
    {
        var consulta = _context.Produtos
            .Where(p => p.Preco < preco)
            .AsQueryable();

        if (pagina != null)
            consulta = consulta.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);

        return [.. consulta];
    }

    public List<ProdutoModel> ListarProdutos(int? pagina)
    {
        var consulta = _context.Produtos.AsQueryable();

        if (pagina != null)
        {
            consulta = consulta.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
        }

        return [.. consulta];
    }
}