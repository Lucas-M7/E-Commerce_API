using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infrastucture.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Pedidos;

[ApiController]
[Route("api/")]
[Authorize]
public class OrdersController(IPedidoService pedidoService, ConnectContext context) : ControllerBase
{
    private readonly IPedidoService _pedidoService = pedidoService;
    private readonly ConnectContext _context = context;

    /// <summary>
    /// Request a order.
    /// </summary>
    /// <param name="cartId"></param>
    /// <returns></returns>
    [HttpPost("orders")]
    public IActionResult SolicitarPedido([FromQuery] int cartId)
    {
        _pedidoService.SolicitarPedido(cartId);    
        return Ok("Pedido solicitado com sucesso, agora realize o pagamento");
    }

    /// <summary>
    /// Get all orders.
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet("orders")]
    public IActionResult ListarPedidos([FromQuery] int? page)
    {
        var pedido = new List<PedidosModel>();
        var pedidos = _pedidoService.ListarPedidosPendentes(page);

        foreach (var item in pedidos)
        {
            pedido.Add(new PedidosModel
            {
                Id = item.Id,
                ProdutoNome = item.ProdutoNome,
                ProdutoPreco = item.ProdutoPreco,
                ProdutoId = item.ProdutoId,
                Quantidade = item.Quantidade,
                ValorUnitario = item.ValorUnitario,
                Status = item.Status
            });
        }

        return Ok(pedido);
    }

    /// <summary>
    /// Cancel a order.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpDelete("orders")]
    public IActionResult CancelarPedido([FromQuery] int orderId)
    {
        if (_context.Pedidos.Any(x => x.Id == orderId))
        {
            _pedidoService.CancelarPedido(orderId);
            return Ok("Pedido cancelado com sucesso.");
        }
        return NotFound("Pedido não encontrado, verifique o ID.");
    }
}