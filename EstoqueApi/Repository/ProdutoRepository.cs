using EstoqueApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly SqLiteDbContext _context;
        public ProdutoRepository(SqLiteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProdutoEntity>>ListarProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<ProdutoEntity> RecuperarProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                throw new Exception("Produto não foi encontrado");

            return produto;
        }

        public async Task<int> EditarProduto(ProdutoEntity produto)
        {
            try
            {
                _context.Entry(produto).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            catch 
            {
                if (!ProdutoExists(produto.Id))
                {
                    throw new Exception("Produto não foi encontrado");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }

        public async Task<int> CriarProduto(ProdutoEntity produto)
        {
            _context.Produtos.Add(produto);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletarProduto(int id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto == null)
            {
                throw new Exception("Produto não foi encontrado");
            }
            _context.Produtos.Remove(produto);
            return await _context.SaveChangesAsync();
        }
    }
}
