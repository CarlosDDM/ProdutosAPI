using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdutosAPI.Models;

public class Produto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{ get; set; }

    [Column(TypeName = "varchar(50)")]
    public string Nome { get; set; }

    [Column(TypeName = "varchar(15)")]
    public string Tipo { get; set; }

    public float PrecoUnitario { get; set; }
}
