using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvc.Models;

public class Produto
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string? Imagem { get; set; }
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string? Valor { get; set; }
}