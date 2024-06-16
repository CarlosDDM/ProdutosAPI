using System.ComponentModel.DataAnnotations;

namespace ProdutosAPI.Data.Dtos;

public class ReadProdutoDto
{

    public int Id { get; set; }
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public float PrecoUnitario { get; set; }
}
