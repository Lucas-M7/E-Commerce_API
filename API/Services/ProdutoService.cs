using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infrastucture.DB;

namespace API.Services;

public class ProdutoService(ConnectContext context) : IProdutoService
{
    private readonly ConnectContext _context = context;

    public List<ProdutoModel> ListarProdutos(int? pagina)
    {
        var consulta = _context.Produtos.AsQueryable();

        int itensPorPagina = 20;

        if (pagina != null)
        {
            consulta = consulta.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
        }

        return [.. consulta];
    }
}