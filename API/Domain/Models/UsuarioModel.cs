using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Models;


public class UsuarioModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; } = default!;

    [Required]
    [StringLength(255)]
    public string Email { get; set; } = default!;

    [Required]
    [StringLength(255)]
    public string Nome { get; set; } = default!;

    [Required]
    [StringLength(15)]
    public string Senha { get; set; } = default!;
}