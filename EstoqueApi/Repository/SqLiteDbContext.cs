using EstoqueApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Repository
{
    public class SqLiteDbContext : DbContext
    {
        public DbSet<ProdutoEntity> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Estoque.sqlite");
        }
    }
}
