using API.Domain.DTOs;
using API.Domain.Models;

namespace API.Domain.Interfaces;

public interface IPedidoService
{
    void SolicitarPedido(int carrinhoId);
    void CancelarPedido(int pedidoId);
    void AtualizarStatusDoPedido(int pedidoId, PagamentoDTO cartaoDTO, string status);
    List<PedidoDTO> ListarPedidosPendentes(int? pagina);
}