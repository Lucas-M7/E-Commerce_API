using API.Domain.Model;
using API.Domain.Models;

namespace API.Domain.Interfaces;

public interface IPedidoService
{
    PedidosModel CriarPedido(PagamentoSolicitacaoModel solicitacao);
    void AtualizarPedido(PedidosModel pedido);
    void ApagarPedido(string pedidoId);
}