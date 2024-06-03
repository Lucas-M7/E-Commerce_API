using API.Domain.Models;
using API.Domain.ModelViews;
using API.Infrastucture.DB;

namespace API.Services.Validations;

public class PagamentoValidador(ConnectContext context) : WishlistService(context)
{

    public ErrorValidacao PagamentoValidacao(PagamentoSolicitacaoModel solicitacao, int produtoId)
    {
        var validacao = new ErrorValidacao()
        {
            Mensagens = []
        };

        if (string.IsNullOrEmpty(solicitacao.NumeroCartao) || solicitacao.NumeroCartao.Length > 16 || solicitacao.NumeroCartao.Length < 15)
            validacao.Mensagens.Add("Cartão inválido, digite somente números, sem espaços.");

        if (string.IsNullOrEmpty(solicitacao.NomeTitular))
            validacao.Mensagens.Add("O nome do títular não pode ficar vazio.");

        if (string.IsNullOrEmpty(solicitacao.DataValidade) || solicitacao.DataValidade.Length != 5)
            validacao.Mensagens.Add("A data de validade não pode ser vazia ou uma data inválida.");

        if (string.IsNullOrEmpty(solicitacao.Cvv) || solicitacao.Cvv.Length != 3)
            validacao.Mensagens.Add("O cvv não pode ser vazio ou diferente de 3 caractéres.");

        if (solicitacao.Montante <= 0)
            validacao.Mensagens.Add("O seu montante não pode ser menor ou igual a zero."); 

        var produto = ObterProduto(produtoId);
        if (produto == null)
            validacao.Mensagens.Add("Produo não encontrado.");
        else if (solicitacao.Montante < (decimal)produto.Preco)
            validacao.Mensagens.Add("Montante baixo.");        

        return validacao;    
    }
}