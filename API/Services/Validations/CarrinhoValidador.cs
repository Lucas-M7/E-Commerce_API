using API.Domain.ModelViews;
using API.Infrastucture.DB;

namespace API.Services.Validations;

public class CarrinhoValidador(ConnectContext context)
{
    private readonly ConnectContext _context = context;

    public ErrorValidacao ValidacaoAdicionarAoCarrinho(String usuarioNome, int produtoId, int quantidade)
    {
        var validacao = new ErrorValidacao()
        {
            Mensagens = []
        };

        if (string.IsNullOrEmpty(usuarioNome))
            validacao.Mensagens.Add("Nome de usuário não pode ser vazio.");

        if (produtoId <= 0)
            validacao.Mensagens.Add("ID do produto inválido.");

        if (_context.Produtos.Any(x => x.Estoque < quantidade))
            validacao.Mensagens.Add("Quantidade desejada maior que o estoque.");

        if (quantidade <= 0)
            validacao.Mensagens.Add("Quantidade não pode ser menor ou igual a zero.");

        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoID == produtoId);
        if (produto == null)
            validacao.Mensagens.Add("Produto não encontrado.");

        return validacao;
    }

    public ErrorValidacao ValidarRemoverDoCarrinho(int carrinhoId, int quantidade)
    {
        var validacao = new ErrorValidacao()
        {
            Mensagens = []
        };

        if (carrinhoId <= 0)
            validacao.Mensagens.Add("ID do carrinho inválido.");

        if (quantidade <= 0)
            validacao.Mensagens.Add("Quantidade deve ser maior que zero.");

        var carrinhoItem = _context.Carrinho.FirstOrDefault(c => c.ID == carrinhoId);
        if (carrinhoItem == null)
            validacao.Mensagens.Add("Item do carrinho não encontrado.");

        if (carrinhoItem != null && quantidade > carrinhoItem.Quantidade)
            validacao.Mensagens.Add("Quantidade inválida. A quantidade a remover é maior do que a disponível no carrinho.");

        return validacao;
    }
}