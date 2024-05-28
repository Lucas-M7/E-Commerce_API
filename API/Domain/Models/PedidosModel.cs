namespace API.Domain.Model;
public class PedidosModel
{
    public string Id { get; set; } = default!;
    public decimal MontanteTotal { get; set; }
    public string Status { get; set; } = default!;
    public DateTime PedidoEm { get; set; }
}