using API.Domain.Models;

namespace API.Domain.Interfaces;

public interface ICarrinhoService
{
    void AdicionarAoCarrinho(string usuarioNome, int produtoID, int quantidade);
    void RemoverDoCarrinho(int carrinhoId, int quantidade);
    List<CarrinhoModel> ListarItensNoCarrinho(int? pagina);
}