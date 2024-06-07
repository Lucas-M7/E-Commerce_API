using API.Domain.DTOs;
using API.Domain.Interfaces;
using API.Infrastucture.DB;
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

    [HttpPost("payment")]
    public IActionResult RealizarPagamento([FromQuery] int pedidoId, [FromBody] PagamentoDTO cartaoDTO)
    {
        var validacao = _pagamentoValidador.ValidacaoPagamento(cartaoDTO);

        if (validacao.Mensagens.Count != 0)
            return BadRequest(validacao);

        var pagamento = new PagamentoDTO
        {
           NumeroCartao = cartaoDTO.NumeroCartao,
           DataValidade = cartaoDTO.DataValidade,
           CVV = cartaoDTO.CVV,
           NomeTitular = cartaoDTO.NomeTitular
        };

        _pedidoService.AtualizarStatusDoPedido(pedidoId, pagamento, "Pago");
        return Ok("Pagamento realizado com sucesso.");
    }
}