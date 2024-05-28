namespace API.Domain.Models;

public class PagamentoRespostaModel
{
    public bool Sucesso { get; set; } = default!;
    public string TransacaoId { get; set; } = default!;
    public string Mensagem { get; set; } = default!;
}