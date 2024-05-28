using API.Domain.Models;

namespace API.Domain.Interfaces;

public interface IPagamentoServico
{
    public Task<PagamentoRespostaModel> ProcessarPagamento(PagamentoSolicitacaoModel solicitacao, int produtoId);
}