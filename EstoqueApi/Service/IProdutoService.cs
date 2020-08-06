using EstoqueApi.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Service
{
    public interface IProdutoService
    {
        public Task<IEnumerable<ProdutoDTO>> ListarProdutos();
        public Task<ProdutoDTO> RecuperarProduto(int id);
        public Task<int> EditarProduto(ProdutoDTO produtoDTO);
        public Task<int> CriaProduto(ProdutoDTO produtoDTO);
        public Task<int> DeletarProduto(int id);
    }
}
