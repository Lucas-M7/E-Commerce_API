namespace API.Domain.DTOs;

public class PagamentoDTO
{
    public string NumeroCartao { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public string DataValidade { get; set; } = default!;
    public string NomeTitular { get; set; } = default!;
}