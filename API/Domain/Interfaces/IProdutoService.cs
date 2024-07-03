using API.Domain.Models;

namespace API.Domain.Interfaces;

public interface IProdutoService
{
    List<ProdutoModel> ListarProdutos(int? pagina);
    List<ProdutoModel> BuscarProdutoPelaCategoria(string categoria, int? pagina);
}