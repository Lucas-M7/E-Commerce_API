using API.Domain.Interfaces;
using API.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Pedidos;

[Authorize]
[ApiController]
[Route("api/")]
public class PedidosController(IPagamentoService pagamentoServico, IPedidoService pedidoServico) : ControllerBase
{
    private readonly IPagamentoService _pagamentoServico = pagamentoServico;
    private readonly IPedidoService _pedidoServico = pedidoServico;

    /// <summary>
    /// Realização do pedido com aprovação na mesma hora.
    /// </summary>
    /// <param name="solicitacao"></param>
    /// <param name="produtoId"></param>
    /// <returns></returns>
    [HttpPost("pedidos/{produtoId}")]
    public IActionResult CriacaoDoPedido([FromBody] PagamentoSolicitacaoModel solicitacao, int produtoId)
    {
        var pedidoCriado = _pedidoServico.CriarPedido(solicitacao);

        var pagamentoSolicitado = new PagamentoSolicitacaoModel
        {
            NumeroCartao = solicitacao.NumeroCartao,
            NomeTitular = solicitacao.NomeTitular,
            DataValidade = solicitacao.DataValidade,
            Cvv = solicitacao.Cvv,
            Montante = solicitacao.Montante
        };

        var pagementoResposta = _pagamentoServico.ProcessarPagamento(pagamentoSolicitado, produtoId);

        if (pagementoResposta.Result.Sucesso)
        {
            pedidoCriado.Status = "Pago";
            _pedidoServico.AtualizarPedido(pedidoCriado);
            return Ok(new 
            {
                PedidoId = pedidoCriado.Id, Mensagem = "Pedido criado e pago com sucesso."
            });
        }
        else
        {
            _pedidoServico.ApagarPedido(pedidoCriado.Id);
            return BadRequest(new
            {
                Mensagem = $"Falha no pagamento: {pagementoResposta.Result.Mensagem}"
            });
        }
    }
}