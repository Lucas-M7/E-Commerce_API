using API.Domain.ModelViews;
using API.Infrastucture.DB;

namespace API.Services.Validations;

public class WishlistValidador(ConnectContext context)
{
    private readonly ConnectContext _context = context;

    public ErrorValidacao ValidacaoAdicionarALista(int produtoId)
    {
        var validacao = new ErrorValidacao
        {
            Mensagens = []
        };

        if (produtoId <= 0)
            validacao.Mensagens.Add("ID do produto inválido, não pode ser igual ou menor que zero.");

        var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoID == produtoId);
        if (produto == null)
            validacao.Mensagens.Add("Produto não encontrado, verifique o ID.");   

        return validacao;     
    }

    public ErrorValidacao ValidacaoRemoverDaLista(int listaId)
    {
        var validacao = new ErrorValidacao
        {
            Mensagens = []
        };

        if (listaId <= 0)
            validacao.Mensagens.Add("ID do produto inválido, não pode ser igual ou menor que zero.");

        var lista = _context.Wishlist.FirstOrDefault(x => x.Id == listaId);
        if (lista == null)
            validacao.Mensagens.Add("Lista não encontrada.");

        return validacao;    
    }
}