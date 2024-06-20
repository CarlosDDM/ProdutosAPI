using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProdutosAPI.Data;
using ProdutosAPI.Data.Dtos;
using ProdutosAPI.Models;
using System.Collections;

namespace ProdutosAPI.Servicos;

public class ProdutoServico
{
    private ProdutoContext _context;
    private IMapper _mapper;
    public ProdutoServico(ProdutoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Produto> CadastraProduto(CreateProdutoDto dto)
    {
        Produto produto = _mapper.Map<Produto>(dto);

        _context.Add(produto);

        await _context.SaveChangesAsync();

        return produto;
    }

    public List<ReadProdutoDto> MostraProduto()
    {
        var produtoDto = _mapper.Map<List<ReadProdutoDto>>(_context.Produtos.ToList());

        return produtoDto;

    }

    public ReadProdutoDto? MostraProdutoPorId(int id)
    {
        var existe = RecuperaId(id);

        if (existe != null)
        {
            ReadProdutoDto produtoDto = _mapper.Map<ReadProdutoDto>(existe);

            return produtoDto;
        }

        return null;

    }

    public Produto? RecuperaId(int id) 
    {

        return _context.Produtos.FirstOrDefault(produto => produto.Id == id);

    }

    public bool AtualizaProduto(int id, UpdateProdutoDto produtoDto)
    {
        var produto = RecuperaId(id);

        bool resultado = false;

        if(produto != null)
        { 
            _mapper.Map(produtoDto, produto);
            _context.SaveChanges();
            resultado = true;

            return resultado;
        }

        return resultado;
    }
    /*
    public bool AtualizaProdutoParcialmente(int id, JsonPatchDocument<UpdateProdutoDto> patch)
    {
        var produto = RecuperaId(id);
        if (produto != null) 
        {
            var produtoParaAtualizar = _mapper.Map<UpdateProdutoDto>(produto);

            patch.ApplyTo(produtoParaAtualizar, ModelState);

            if (!TryValidateModel(produtoParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(produtoParaAtualizar, produto);
            _context.SaveChanges();

        }
    }
    */

    public bool DeletaProduto(int id)
    {
        var produto = RecuperaId(id);
        bool resultado = false;

        if (produto != null) 
        {
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            resultado = true;

            return resultado;
        }
        return resultado;
    }

    public IEnumerable RecuperaDadosDashboard()
    {
        var dadoDashboard = _context.Produtos
            .GroupBy(produto => produto.Tipo)
            .Select(produto => new
            {
                Tipo = produto.Key,
                Quantidade = produto.Count(),
                MediaPrecoUnitario = produto.Average(m => m.PrecoUnitario)
            }).ToList();

        return dadoDashboard;
    }
 }

