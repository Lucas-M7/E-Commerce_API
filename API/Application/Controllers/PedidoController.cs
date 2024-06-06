using API.Domain.DTOs;
using API.Domain.Interfaces;
using API.Infrastucture.DB;
using API.Services.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers;

[ApiController]
[Route("api/")]

public class PedidoController(IPedidoService pedidoService, ConnectContext context) : ControllerBase
{
    private readonly IPedidoService _pedidoService = pedidoService;
    private readonly ConnectContext _context = context;

    [HttpPost("pedidos/")]
    public IActionResult SolicitarPedido(int carrinhoId)
    {
        _pedidoService.SolicitarPedido(carrinhoId);    
        return Ok("Pedido solicitado com sucesso, agora realize o pagamento");
    }
}