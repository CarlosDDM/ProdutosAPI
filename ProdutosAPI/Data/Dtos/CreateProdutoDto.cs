using System.ComponentModel.DataAnnotations;
using System.IO.Compression;

namespace ProdutosAPI.Data.Dtos;

public class CreateProdutoDto
{ 
    [Required(ErrorMessage = "O nome do seu produto é obrigatório")]
    [StringLength(50, ErrorMessage = "O nome do seu produto é maior que 50 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O tipo do seu produto é obrigatório")]
    [AllowedValues("Material", "Serviço", ErrorMessage = "Esse tipo não corresponde a um dos tipos validos")]
    public string Tipo { get; set; }

    [Required(ErrorMessage = "O preço do seu produto é obrigatório")]
    [Range (0, float.MaxValue, ErrorMessage = "Seu número é muito pequeno ou muito grande")]
    public float PrecoUnitario { get; set; }
}
