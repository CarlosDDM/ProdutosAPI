using AutoMapper;
using ProdutosAPI.Data.Dtos;
using ProdutosAPI.Models;

namespace ProdutosAPI.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile() 
    {

        CreateMap<CreateProdutoDto, Produto>(); //POST
        CreateMap<Produto, ReadProdutoDto>();   //GET
        CreateMap<UpdateProdutoDto, Produto>(); //UPDATE PUT
        CreateMap<Produto, UpdateProdutoDto>(); //UPDATE PATCH
    }
}
