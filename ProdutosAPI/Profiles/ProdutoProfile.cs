using AutoMapper;
using ProdutosAPI.Data.Dtos;
using ProdutosAPI.Models;

namespace ProdutosAPI.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile() 
    {

        CreateMap<CreateProdutoDto, Produto>(); 
        CreateMap<Produto, ReadProdutoDto>();  
        CreateMap<UpdateProdutoDto, Produto>(); 
    }
}
