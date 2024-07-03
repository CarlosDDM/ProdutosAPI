using ProdutosAPI.Data.Dtos;
using ProdutosAPI.Models;
using System.Collections;

namespace ProdutosAPI.Services.Interfaces;

public interface IProdutoServico
{
    Task<Produto> CadastraProduto(CreateProdutoDto dto);
    Task<List<ReadProdutoDto>> MostraProduto();
    Task<ReadProdutoDto?> MostraProdutoPorId(int id);
    Task<bool> AtualizaProduto(int id, UpdateProdutoDto produtoDto);
    Task<bool> DeletaProduto(int id);
    Task<IEnumerable> RecuperaDadosDashboard();
}
