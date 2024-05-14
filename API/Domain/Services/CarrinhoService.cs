using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infrastucture.DB;

namespace API.Domain.Services;

public class CarrinhoService(ConnectContext context) : ICarrinhoService
{
    private readonly ConnectContext _context = context;

    public void AdicionarAoCarrinho(int usuarioId, int produtoId, int quantidade)
    {
        var carrinhoItem = _context.Carrinho.FirstOrDefault
            (c => c.UsuarioID == usuarioId && c.ProdutoID == produtoId);

        if (carrinhoItem == null)
        {
            carrinhoItem = new CarrinhoModel
            {
                UsuarioID = usuarioId,
                ProdutoID = produtoId,
                Quantidade = quantidade
            };
            _context.Carrinho.Add(carrinhoItem);
        }
        else
        {
            carrinhoItem.Quantidade += quantidade;
        }

        _context.SaveChanges();
    }
}