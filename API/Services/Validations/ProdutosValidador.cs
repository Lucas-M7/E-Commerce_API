using API.Domain.ModelViews;
using API.Infrastucture.DB;

namespace API.Services.Validations;

public class ProdutosValidador
{
    private readonly ConnectContext _context;

    public ProdutosValidador(ConnectContext context)
    {
        _context = context;
    }

    public ErrorValidacao ValidacaoBuscarCategoria(string categoria)
    {
        var validacao = new ErrorValidacao()
        {
            Mensagens = []
        };

        if (!_context.Produtos.Any(x => x.Categoria == categoria))
            validacao.Mensagens.Add("Categoria não encontrada.");

        if (string.IsNullOrEmpty(categoria))
            validacao.Mensagens.Add("Categoria não informada.");

        return validacao;
    }

    public ErrorValidacao ValidacaoBuscarPorPreco(double preco)
    {
        var validacao = new ErrorValidacao()
        {
            Mensagens = []
        };

        if (!_context.Produtos.Any(x => x.Preco == preco))
            validacao.Mensagens.Add("Não foi encontrado nenhum produto com esse preço");

        return validacao;
    }
}