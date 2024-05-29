using API.Domain.Interfaces;
using API.Domain.Model;
using API.Domain.Models;

namespace API.Services;

public class PedidoService : IPedidoService
{
    private static List<PedidosModel> _pedidos = [];
    
    public void ApagarPedido(string pedidoId)
    {
        var pedido = _pedidos.FirstOrDefault(x => x.Id == pedidoId);
        if (pedido != null)
        {
            _pedidos.Remove(pedido);
        }
    }

    public void AtualizarPedido(PedidosModel pedido)
    {
        var pedidoExistente = _pedidos.FirstOrDefault(x => x.Id == pedido.Id);
        if (pedidoExistente != null)
        {
            pedidoExistente.MontanteTotal = pedido.MontanteTotal;
            pedidoExistente.Status = pedido.Status;
            pedidoExistente.PedidoEm = pedido.PedidoEm;
        }
    }

    public PedidosModel CriarPedido(PagamentoSolicitacaoModel solicitacao)
    {
        var novoPedido = new PedidosModel
        {
            Id = Guid.NewGuid().ToString(),
            MontanteTotal = solicitacao.Montante,
            Status = "Pendente",
            PedidoEm = DateTime.UtcNow
        };
        
        _pedidos.Add(novoPedido);
        return novoPedido;
    }
}