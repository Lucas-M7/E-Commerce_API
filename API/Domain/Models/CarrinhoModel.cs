using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Models;

public class CarrinhoModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; } = default!;

    [Required]
    public int UsuarioID { get; set; } = default!;

    [Required]
    public string UsuarioNome { get; set; } = default!;

    [Required]
    public int ProdutoID { get; set; } = default!;

    [Required]
    public string ProdutoNome { get; set; } = default!;

    [Required]
    public double ProdutoPreco { get; set; } = default!;

    [Required]
    public int Quantidade { get; set; } = default!;
}