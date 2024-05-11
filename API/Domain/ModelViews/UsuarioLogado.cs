namespace API.Domain.ModelViews;

public class UsuarioLogado
{
    public string Nome { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Token { get; set; } = default!;
}