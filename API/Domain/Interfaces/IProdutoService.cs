using API.Domain.Models;

namespace API.Domain.Interfaces;

public interface IProdutoService
{
    List<ProdutoModel> ListarProdutos(int? pagina);
    List<ProdutoModel> BuscarProdutoPelaCategoria(string categoria, int? pagina);
    List<ProdutoModel> BuscarProdutoPeloPreco(double preco, int? pagina);
    List<ProdutoModel> BuscarProdutoPeloNome(string nome, int? pagina);
}