using API.Domain.DTOs;
using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infrastucture.DB;

namespace API.Services;

public class PedidoService(ConnectContext context) : IPedidoService
{
    private readonly ConnectContext _context = context;

    public void SolicitarPedido(int carrinhoId)
    {
        var carrinho = ObterCarrinho(carrinhoId);

        var pedido = new PedidosModel
        {
            CarrinhoId = carrinho.ID,
            ProdutoNome = carrinho.ProdutoNome,
            ProdutoPreco = carrinho.ProdutoPreco,
            ProdutoId = carrinho.ProdutoID,
            ValorUnitario = carrinho.Total,
            Quantidade = carrinho.Quantidade,
            Status = "Pendente",
        };

        _context.Pedidos.Add(pedido);
        _context.SaveChanges();
    }

    public void CancelarPedido(int pedidoId)
    {
        var pedido = ObterPedido(pedidoId);

        _context.Remove(pedido);
        _context.SaveChanges();
    }

    public void AtualizarStatusDoPedido(int pedidoId, PagamentoDTO cartaoDTO, string novoStatus)
    {
        var pedido = ObterPedido(pedidoId);

        pedido.Status = novoStatus;

        if (novoStatus.Equals("pago", StringComparison.CurrentCultureIgnoreCase))
        {
            var carrinho = _context.Carrinho.FirstOrDefault(x => x.ID == pedido.CarrinhoId);
            if (carrinho != null)
                _context.Carrinho.Remove(carrinho);

            _context.Pedidos.Remove(pedido);
        }

        _context.SaveChanges();
    }

    public List<PedidoDTO> ListarPedidosPendentes(int? pagina)
    {
        var consulta = _context.Pedidos
        .Where(p => p.Status == "Pendente")
        .Select(p => new PedidoDTO
        {
            Id = p.Id,
            ProdutoNome = p.ProdutoNome,
            ProdutoPreco = p.ProdutoPreco,
            ProdutoId = p.ProdutoId,
            Quantidade = p.Quantidade,
            ValorUnitario = p.ValorUnitario,
            Status = p.Status
        });

        int itensPorPagina = 10;

        if (pagina != null)
        {
            consulta = consulta.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
        }

        return [.. consulta];
    }

    private CarrinhoModel ObterCarrinho(int carrinhoId)
    {
        return _context.Carrinho.FirstOrDefault(x => x.ID == carrinhoId)
            ?? throw new FileNotFoundException("Carrinho não encontrado, verifique o ID.");
    }

    private PedidosModel ObterPedido(int pedidoId)
    {
        return _context.Pedidos.FirstOrDefault(x => x.Id == pedidoId)
            ?? throw new FileNotFoundException("Pedido não encontrado.");

    }
}