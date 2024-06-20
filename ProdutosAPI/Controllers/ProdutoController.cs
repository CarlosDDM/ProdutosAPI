using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutosAPI.Data.Dtos;
using ProdutosAPI.Servicos;

namespace ProdutosAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]
[Authorize]
public class ProdutoController : ControllerBase
{
    private readonly ProdutoServico _servico;
    public ProdutoController(ProdutoServico servico)
    {
        _servico = servico;
    }
    /// <summary>
    /// Adiciona um produto ao banco de dados
    /// </summary>
    /// <param name="produtoDto">Objeto com os campos necessários para criação de um produto</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AdicionaProduto([FromBody] CreateProdutoDto produtoDto)
    {

        var produto = await _servico.CadastraProduto(produtoDto);

        return CreatedAtAction(nameof(RecuperaProdutoPorId), new { id = produto.Id }, produto);
    }

    /// <summary>
    /// Recupera todos os produtos do banco de dados
    /// </summary>
    /// <param name="skip">Pode te ajudar a pular quantos resultados desejar</param>
    /// <param name="take">Pode te ajudar a recuperar os dados em uma quantidade</param>
    /// <returns> List </returns>
    /// <response code="200">Caso a busca seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadProdutoDto> RecuperaTodosOsProdutos([FromHeader] int skip = 0, [FromHeader] int take = 50)
    {

        return _servico.MostraProduto().Skip(skip).Take(take).ToList();
    }

    /// <summary>
    /// Recupera um produtos do banco de dados
    /// </summary>
    /// <param name="id">Parametro necessario para poder executar a busca no banco de dados</param>
    /// <returns> IActionResult </returns>
    /// <response code="200">Caso a busca seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RecuperaProdutoPorId(int id)
    {
        var produto = _servico.MostraProdutoPorId(id);

        if (produto == null)
        {
            return NotFound("O id passado não foi encontrado");
        }

        return Ok(produto);
    }


    /// <summary>
    /// Atualiza o objeto que foi para o banco de dados
    /// </summary>
    /// <param name="id">Parametro necessario para executar a busca do objeto desejado no banco de dados</param>
    /// <param name="produtoDto">Parametro necessario para passar os dados de atualização</param>
    /// <returns> IActionResult </returns>
    /// <response code="204">Caso a atualização do objeto seja feito com sucesso</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtuzalizaProduto(int id, [FromBody] UpdateProdutoDto produtoDto)
    {
        var resultado =  _servico.AtualizaProduto(id, produtoDto);
        if (resultado)
        {
            return NoContent();

        } 
        return NotFound("Id do produto não encontrado na base de dados");

    }

    /// <summary>
    /// Deleta o objeto do banco de dados
    /// </summary>
    /// <param name="id">Parametro necessario para buscar e deletar no banco de dados</param>
    /// <returns> IActionResult </returns>
    /// <response code="204">Caso o objeto tenha sido deletado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletaProduto(int id)
    {
        bool resultado = _servico.DeletaProduto(id);

        if(resultado == false)
        {
            return NotFound("Produto para ser deletado não encontrado");
        }

        return NoContent();
    }

    /// <summary>
    /// Faz uma query no banco onde busca o preço unitário médio segregado por tipo
    /// </summary>
    /// <returns> IActionResult </returns>
    /// <response code="200">Caso o objeto tenha sido deletado</response>
    [HttpGet("dashboard")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Dashboard()
    {
        var dashboard = _servico.RecuperaDadosDashboard();

        return Ok(dashboard);

    }

}