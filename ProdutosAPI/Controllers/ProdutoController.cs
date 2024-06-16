using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProdutosAPI.Data;
using ProdutosAPI.Data.Dtos;
using ProdutosAPI.Models;

namespace ProdutosAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ProdutoController : ControllerBase
{
    private ProdutoContext _context;
    private IMapper _mapper;
    public ProdutoController(ProdutoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaProduto([FromBody] CreateProdutoDto produtoDto)
    {

        Produto produto = _mapper.Map<Produto>(produtoDto);

        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaProdutoPorId), new { id = produto.Id }, produto);
    }

    [HttpGet]
    public IEnumerable<ReadProdutoDto> RecuperaTodosOsProdutos([FromHeader] int skip = 0, [FromHeader] int take = 50)
    {

        return _mapper.Map<List<ReadProdutoDto>>(_context.Produtos.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaProdutoPorId(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);

        if (produto == null)
        {
            return NotFound("O id passado não foi encontrado");
        }

        var produtoDto = _mapper.Map<ReadProdutoDto>(produto);
        return Ok(produtoDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtuzalizaProduto(int id, [FromBody] UpdateProdutoDto produtoDto)
    {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
        if (produto == null)
        {
            return NotFound("Produto para ser atualizado não encontrado");
        }

        _mapper.Map(produtoDto, produto);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaParcialmenteProduto(int id, JsonPatchDocument<UpdateProdutoDto> patch)
    {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
        if (produto == null) 
        {
            return NotFound("O id passado não foi encontrado");
        }

        var produtoParaAtualizar = _mapper.Map<UpdateProdutoDto>(produto);
        
        patch.ApplyTo(produtoParaAtualizar, ModelState);
        
        if(!TryValidateModel(produtoParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(produtoParaAtualizar, produto);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaProduto(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);

        if(produto == null)
        {
            return NotFound("Produto para ser deletado não encontrado");
        }

        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return NoContent();
    }

}