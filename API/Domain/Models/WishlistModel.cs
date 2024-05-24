using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Models;

public class WishlistModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(150)]
    public string ProdutoNome { get; set; } = default!;

    [Required]
    public double ProdutoPreco { get; set; } = default!;
}