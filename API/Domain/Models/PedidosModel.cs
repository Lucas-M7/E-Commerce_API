using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Models;

public class PedidosModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int CarrinhoId { get; set; } = default!;


    [Required]
    public string ProdutoNome { get; set; } = default!;

    [Required]
    public double ProdutoPreco { get; set; } = default!;

    [Required]
    public int ProdutoId { get; set; } = default!;

    [Required]
    public int Quantidade { get; set; } = default!;

    [Required]
    public double ValorUnitario { get; set; } = default!;

    [Required]
    public string Status { get; set; } = default!;
}