using API.Domain.Interfaces;
using API.Domain.Models;
using API.Services.Validations;

namespace API.Services;
public class PagamentoService(PagamentoValidador validador) : IPagamentoService
{
    private readonly PagamentoValidador _validador = validador;

    public Task<PagamentoRespostaModel> ProcessarPagamento(PagamentoSolicitacaoModel solicitacao, int produtoId)
    {
        var validacao =  _validador.PagamentoValidacao(solicitacao, produtoId);

        if (validacao.Mensagens.Count > 0)
        {
            return Task.FromResult(new PagamentoRespostaModel
            {
                Sucesso = false,
                TransacaoId = string.Empty,
                Mensagem = string.Join(" ", validacao.Mensagens)
            });
        }

        var transacaoId = Guid.NewGuid().ToString();
        return Task.FromResult(new PagamentoRespostaModel
        {
            Sucesso = true,
            TransacaoId = transacaoId,
            Mensagem = "Pagamento processado com sucesso."
        });
    }
}