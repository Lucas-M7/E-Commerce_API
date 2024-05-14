namespace API.Domain.Interfaces;

public interface ICarrinhoService
{
    void AdicionarAoCarrinho(int usuarioId, int produtoId, int quantidade);
}