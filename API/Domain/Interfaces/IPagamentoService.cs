using API.Domain.Models;

namespace API.Domain.Interfaces;

public interface IPagamentoService
{
    public Task<PagamentoRespostaModel> ProcessarPagamento(PagamentoSolicitacaoModel solicitacao, int produtoId);
}