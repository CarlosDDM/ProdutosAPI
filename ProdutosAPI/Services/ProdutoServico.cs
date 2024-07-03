using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProdutosAPI.Data;
using ProdutosAPI.Data.Dtos;
using ProdutosAPI.Models;
using ProdutosAPI.Services.Interfaces;
using System.Collections;

namespace ProdutosAPI.Servicos;

public class ProdutoServico : IProdutoServico
{
    private readonly ProdutoContext _context;
    private readonly IMapper _mapper;
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

    public async Task<List<ReadProdutoDto>> MostraProduto()
    {
        var produtoDto = _mapper.Map<List<ReadProdutoDto>>(await _context.Produtos.ToListAsync());

        return produtoDto;

    }

    public async Task<ReadProdutoDto?> MostraProdutoPorId(int id)
    {
        var existe = await RecuperaId(id);

        if (existe != null)
        {
            ReadProdutoDto produtoDto = _mapper.Map<ReadProdutoDto>(existe);

            return produtoDto;
        }

        return null;

    }

    public async Task<Produto?> RecuperaId(int id) 
    {

        return await _context.Produtos.FirstOrDefaultAsync(produto => produto.Id == id);

    }

    public async Task<bool> AtualizaProduto(int id, UpdateProdutoDto produtoDto)
    {
        var produto = await RecuperaId(id);

        bool resultado = false;

        if(produto != null)
        { 
            _mapper.Map(produtoDto, produto);
            await _context.SaveChangesAsync();
            resultado = true;

            return resultado;
        }

        return resultado;
    }

    public async Task<bool> DeletaProduto(int id)
    {
        var produto = await RecuperaId(id);
        bool resultado = false;

        if (produto != null) 
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            resultado = true;

            return resultado;
        }
        return resultado;
    }

    public async Task<IEnumerable> RecuperaDadosDashboard()
    {
        var dadoDashboard = await _context.Produtos
            .GroupBy(produto => produto.Tipo)
            .Select(produto => new
            {
                Tipo = produto.Key,
                Quantidade = produto.Count(),
                MediaPrecoUnitario = produto.Average(m => m.PrecoUnitario)
            }).ToListAsync();

        return dadoDashboard;
    }
 }

