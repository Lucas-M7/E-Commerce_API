namespace API.Domain.ModelViews;

public class UsuarioModelView
{
    public int ID { get; set; }
    public string Nome { get; set; } = default!;
    public string Email { get; set; } = default!;
}