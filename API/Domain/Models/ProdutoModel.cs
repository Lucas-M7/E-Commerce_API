using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Models;

public class ProdutoModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; } = default!;

    [Required]
    [StringLength(255)]
    public string Nome { get; set; } = default!;

    [Required]
    [StringLength(255)]
    public string Descricao { get; set; } = default!;

    [Required]
    public double Preco { get; set; } = default!;

    [Required]
    [StringLength(255)]
    public string Categoria { get; set; } = default!;
    
    [Required]
    public int Estoque { get; set; } = default!;
}