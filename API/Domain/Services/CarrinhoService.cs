using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infrastucture.DB;

namespace API.Domain.Services;

public class CarrinhoService(ConnectContext context) : ICarrinhoService
{
    private readonly ConnectContext _context = context;

    public void AdicionarAoCarrinho(string usuarioNome, int produtoId, int quantidade)
    {
        try
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Nome == usuarioNome)
                ?? throw new FileNotFoundException("Usuário não encontrado.");

            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoID == produtoId)
                ?? throw new FileNotFoundException("Produto não encontrado.");

            var carrinhoItem = _context.Carrinho.FirstOrDefault();

            if (carrinhoItem == null)
            {
                carrinhoItem = new CarrinhoModel
                {
                    UsuarioNome = usuario.Nome,
                    ProdutoNome = produto.Nome,
                    Quantidade = quantidade,
                    Total = produto.Preco * quantidade
                };
                _context.Carrinho.Add(carrinhoItem);
            }
            else
            {
                carrinhoItem.Quantidade += quantidade;
                carrinhoItem.Total = produto.Preco * carrinhoItem.Quantidade;
            }

            _context.SaveChanges();
        }
        catch
        {
            throw new BadHttpRequestException("Erro ao adicionar produto ao carrinho.");
        }
    }
}