namespace API.Domain.ModelViews;

public class WishListModelView
{
    public int ListaId { get; set; }
    public int ProdutoId { get; set; } = default!;
    public string ProdutoNome { get; set; } = default!;
    public double ProdutoPreco { get; set; } = default!;
}