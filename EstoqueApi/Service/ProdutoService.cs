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
        private IProdutoRepository produtoRepository;

        public ProdutoService(IProdutoRepository _produtoRepository)
        {
            produtoRepository = _produtoRepository;
        }

        public async Task<IEnumerable<ProdutoDTO>> ListarProdutos()
        {
            var produtos = await produtoRepository.ListarProdutos();
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
            var produto = await produtoRepository.RecuperarProduto(id);

            if(produto == null)
                throw new Exception("Produto não foi encontrado");

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
            return await produtoRepository.EditarProduto(produto);
        }

        public async Task<int> CriaProduto(ProdutoDTO produtoDTO)
        {
            var produto = new ProdutoEntity()
            {
                Nome = produtoDTO.Nome,
                Quantidade = produtoDTO.Quantidade,
                Valor = produtoDTO.Valor
            };
            return await produtoRepository.CriarProduto(produto);
        }

        public async Task<int> DeletarProduto(int id)
        {
            return await produtoRepository.DeletarProduto(id);
        }
    }
}
