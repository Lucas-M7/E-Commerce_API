namespace API.Domain.ModelViews;

public class ProdutoModelView
{
    public int ID { get; set; }
    public string Nome { get; set; } = default!;
    public string Descricao { get; set; } = default!;
    public double Preco { get; set; } = default!;
    public string Categoria { get; set; } = default!;
    public int Estoque { get; set; } = default!;
}