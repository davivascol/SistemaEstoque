using EstoqueApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Repository
{
    public interface IProdutoRepository
    {
        public Task<IEnumerable<ProdutoEntity>> ListarProdutos();
        public Task<ProdutoEntity> RecuperarProduto(int id);
        public Task<int> EditarProduto(ProdutoEntity produto);
        public Task<int> CriarProduto(ProdutoEntity produto);
        public Task<int> DeletarProduto(int id);

    }
}
