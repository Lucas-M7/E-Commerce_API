namespace API.Domain.ModelViews;

public class ProdutoCarrinhoModelView
{
    public int ID { get; set; }
    public string UsuarioNome { get; set; } = default!;
    public string ProdutoNome { get; set; } = default!;
    public double ProdutoPreco { get; set; } = default!;
    public int ProdutoQuantidade { get; set; } = default!;
    public double ValorDoCarrinho { get; set; }
}