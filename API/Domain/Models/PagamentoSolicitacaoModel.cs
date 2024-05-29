namespace API.Domain.Models;

public class PagamentoSolicitacaoModel
{
    public string NumeroCartao { get; set; } = default!;
    public string NomeTitular { get; set; } = default!;
    public string DataValidade { get; set; } = default!;
    public string Cvv { get; set; } = default!;
    public decimal Montante { get; set; } = default!;
}