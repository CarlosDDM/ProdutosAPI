using System.ComponentModel.DataAnnotations;
namespace ProdutosAPI.Data.Dtos;

public class UpdateProdutoDto
{
    [Required(ErrorMessage = "O nome do seu produto é obrigatório")]
    [StringLength(50, ErrorMessage = "O nome do seu produto é maior que 50 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O tipo do seu produto é obrigatório")]
    [AllowedValues("Material", "Serviço", ErrorMessage = "Esse tipo não corresponde a um dos tipos validos." +
        " Parametros validos para tipo: Material, Serviço")]
    public string Tipo { get; set; }

    [Required(ErrorMessage = "O preço do seu produto é obrigatório")]
    [Range(0.1, float.MaxValue, ErrorMessage = "O seu preço unitario deve ser maior que 0.1")]
    public float PrecoUnitario { get; set; }

}
