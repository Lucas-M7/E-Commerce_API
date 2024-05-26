using API.Domain.Models;

namespace API.Domain.Interfaces;

public interface IWishlistService
{
    void AdicionarProdutoNaLista(int produtoId);
    void RemoverProdutoDaLista(int listaId);
    List<WishlistModel> ListarItensDesejados(int? pagina);
}