using Microsoft.EntityFrameworkCore;
using ProdutosAPI.Models;

namespace ProdutosAPI.Data
{
    public class ProdutoContext : DbContext
    {

        public ProdutoContext(DbContextOptions<ProdutoContext> opcoes) : base(opcoes){}

        public DbSet<Produto> Produtos { get; set; }

    }
}
