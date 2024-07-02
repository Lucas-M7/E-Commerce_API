using API.Domain.DTOs;
using API.Domain.Interfaces;
using API.Services.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Payment;

[ApiController]
[Route("api")]
[Authorize]
public class PaymentController(IPedidoService pedidoService, PagamentoValidador pagamentoValidador) : ControllerBase
{
    private readonly PagamentoValidador _pagamentoValidador = pagamentoValidador;
    private readonly IPedidoService _pedidoService = pedidoService;

    /// <summary>
    /// Make a payments
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="cardDTO"></param>
    /// <returns></returns>
    [HttpPost("payment")]
    public IActionResult RealizarPagamento([FromQuery] int orderId, [FromBody] PagamentoDTO cardDTO)
    {
        var validacao = _pagamentoValidador.ValidacaoPagamento(cardDTO);

        if (validacao.Mensagens.Count != 0)
            return BadRequest(validacao);

        var pagamento = new PagamentoDTO
        {
           NumeroCartao = cardDTO.NumeroCartao,
           DataValidade = cardDTO.DataValidade,
           CVV = cardDTO.CVV,
           NomeTitular = cardDTO.NomeTitular
        };

        _pedidoService.AtualizarStatusDoPedido(orderId, pagamento, "Pago");
        return Ok("Pagamento realizado com sucesso.");
    }
}