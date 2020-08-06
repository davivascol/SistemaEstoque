using EstoqueApi.Domain.DTOs;
using EstoqueApi.Domain.Entities;
using EstoqueApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Service
{
    public class ProdutoService : IProdutoService
    {
        private IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<ProdutoDTO>> ListarProdutos()
        {
            var produtos = await _produtoRepository.ListarProdutos();
            var produtosDTO = produtos.Select(p => new ProdutoDTO()
            {
                Id = p.Id,
                Nome = p.Nome,
                Quantidade = p.Quantidade,
                Valor = p.Valor
            });

            return produtosDTO;
        }

        public async Task<ProdutoDTO> RecuperarProduto(int id)
        {
            var produto = await _produtoRepository.RecuperarProduto(id);

            return new ProdutoDTO()
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Quantidade = produto.Quantidade,
                Valor = produto.Valor
            };
        }

        public async Task<int> EditarProduto(ProdutoDTO produtoDTO)
        {
            var produto = new ProdutoEntity()
            {
                Id = produtoDTO.Id,
                Nome = produtoDTO.Nome,
                Quantidade = produtoDTO.Quantidade,
                Valor = produtoDTO.Valor
            };
            return await _produtoRepository.EditarProduto(produto);
        }

        public async Task<int> CriarProduto(ProdutoDTO produtoDTO)
        {
            var produto = new ProdutoEntity()
            {
                Nome = produtoDTO.Nome,
                Quantidade = produtoDTO.Quantidade,
                Valor = produtoDTO.Valor
            };
            return await _produtoRepository.CriarProduto(produto);
        }

        public async Task<int> DeletarProduto(int id)
        {
            return await _produtoRepository.DeletarProduto(id);
        }
    }
}
