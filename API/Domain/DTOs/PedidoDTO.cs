namespace API.Domain.DTOs;

public class PedidoDTO
{
    public int Id { get; set; }
    public string ProdutoNome { get; set; } = default!;
    public double ProdutoPreco { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public double ValorUnitario { get; set; }
    public string Status { get; set; } = default!;
}