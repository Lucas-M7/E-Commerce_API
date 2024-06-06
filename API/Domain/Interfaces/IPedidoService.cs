using API.Domain.DTOs;
using API.Domain.Models;

namespace API.Domain.Interfaces;

public interface IPedidoService
{
    void SolicitarPedido(int carrinhoId);
    void CancelarPedido(int pedidoId);
    List<PedidosModel> ListarPedidosPendentes(int? pagina);
}