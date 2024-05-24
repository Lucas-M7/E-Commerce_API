using API.Domain.Models;

namespace API.Domain.Interfaces;

public interface IWishlistService
{
    void AdicionarNaLista(int produtoId);
    void RemoverDaLista(int produtoId);
    List<WishlistModel> ListaDeDesejo(int? pagina);
}