﻿using EstoqueApi.Domain.DTOs;
using EstoqueApi.Domain.Entities;
using EstoqueApi.Repository;
using EstoqueApi.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EstoqueApi.Tests
{
    public class TestProdutoService
    {
        private IProdutoService _produtoService;
        private Mock<IProdutoRepository> _mockProdutoRepository;

        private void InitializeTest()
        {
            
            _mockProdutoRepository = new Mock<IProdutoRepository>();
            _mockProdutoRepository.Setup(x => x.ListarProdutos()).ReturnsAsync(GetMockLista());
            _mockProdutoRepository.Setup(x => x.RecuperarProduto(It.IsAny<int>())).ReturnsAsync((int id) => MockRecuperaProduto(id));
            _mockProdutoRepository.Setup(x => x.CriarProduto(It.IsAny<ProdutoEntity>())).ReturnsAsync((ProdutoEntity produto) => MockInsereProduto(produto));
            _mockProdutoRepository.Setup(x => x.EditarProduto(It.IsAny<ProdutoEntity>())).ReturnsAsync((ProdutoEntity produto) => MockEditaProduto(produto));
            _mockProdutoRepository.Setup(x => x.DeletarProduto(It.IsAny<int>())).ReturnsAsync((int id) => MockDeleteProduto(id));
            _produtoService = new ProdutoService(_mockProdutoRepository.Object);
        }

        private int MockInsereProduto(ProdutoEntity produtoEntity)
        {
            if (produtoEntity.Nome != null)
                return 1;

            throw new Exception("An error occurred while updating the entries. See the inner exception for details.");
        }

        private int MockEditaProduto(ProdutoEntity produtoEntity)
        {
            var lista = GetMockLista();

            if (lista.Any(i => i.Id == produtoEntity.Id))
                return 1;

            throw new Exception("Produto não foi encontrado");
        }

        private int MockDeleteProduto(int id)
        {
            var lista = GetMockLista();

            if (lista.Any(i => i.Id == id))
                return 1;

            throw new Exception("Produto não foi encontrado");
        }

        private ProdutoEntity MockRecuperaProduto(int id)
        {
            var lista = GetMockLista();

            if (lista.Any(p => p.Id == id))
                return lista.First(p => p.Id == id);

            throw new Exception("Produto não foi encontrado");
        }

        private IEnumerable<ProdutoEntity> GetMockLista() {
            return new List<ProdutoEntity>()
            {
                new ProdutoEntity() { Id = 1,Nome = "Farofa", Quantidade = 50, Valor = 40},
                new ProdutoEntity() { Id = 6,Nome = "Arroz", Quantidade = 30, Valor = 20},
                new ProdutoEntity() { Id = 9,Nome = "Feijao", Quantidade = 40, Valor = 45}
            };
        }

        [Fact]
        public async void TesteListarProdutos()
        {
            InitializeTest();
            var produtos = await _produtoService.ListarProdutos();
            Assert.True(produtos.Count() == 3);
            Assert.True(produtos.Count(p => p.Id == 9) == 1);
            Assert.True(produtos.Count(p => p.Id == 8) == 0);
        }

        [Theory]
        [MemberData(nameof(MockIdExistente))]
        [MemberData(nameof(MockIdInexistente))]
        public async void TesteRecuperarProduto(int id)
        {
            InitializeTest();
            try
            {
                var produto = await _produtoService.RecuperarProduto(id);
                var produtoDTO = new ProdutoDTO() { Id = 1, Nome = "Farofa", Quantidade = 50, Valor = 40 };

                Assert.Equal(produtoDTO.Id, produto.Id);
                Assert.Equal(produtoDTO.Nome, produto.Nome);
                Assert.Equal(produtoDTO.Quantidade, produto.Quantidade);
                Assert.Equal(produtoDTO.Valor, produto.Valor);
            }
            catch (Exception ex)
            {
                Assert.Equal("Produto não foi encontrado", ex.Message);
            }
        }

        public static IEnumerable<object[]> MockIdExistente()
        {
            yield return new object[] { 1 };
        }

        public static IEnumerable<object[]> MockIdInexistente()
        {
            yield return new object[] { -1 };
        }

        [Theory]
        [MemberData(nameof(MockProdutoDTO))]        
        public async void TesteCriarProduto(ProdutoDTO produtoDTO)
        {
            InitializeTest();

            try
            {
                var quantidadeRegistrosAdicionados = await _produtoService.CriarProduto(produtoDTO);
                Assert.Equal(1, quantidadeRegistrosAdicionados);
            }
            catch (Exception ex)
            {
                Assert.Equal("An error occurred while updating the entries. See the inner exception for details.", ex.Message);
            }
        }
                
        public static IEnumerable<object[]> MockProdutoDTO()
        {
            yield return new object[] { new ProdutoDTO() { Nome = "Batata", Quantidade = 43, Valor = 33 } };
            yield return new object[] { new ProdutoDTO() { Quantidade = 43, Valor = 33 } };
        }

        [Theory]
        [MemberData(nameof(MockProdutoDTOEdicao))]
        public async void TesteEditarProduto(ProdutoDTO produtoDTO)
        {
            InitializeTest();

            try
            {
                var quantidadeRegistrosEditados = await _produtoService.EditarProduto(produtoDTO);
                Assert.Equal(1, quantidadeRegistrosEditados);
            }
            catch (Exception ex)
            {
                Assert.Equal("Produto não foi encontrado", ex.Message);
            }
        }

        public static IEnumerable<object[]> MockProdutoDTOEdicao()
        {
            yield return new object[] { new ProdutoDTO() { Id = 1, Nome = "Batata", Quantidade = 43, Valor = 33 } };
            yield return new object[] { new ProdutoDTO() { Id = 10000, Nome = "Batata", Quantidade = 43, Valor = 33 } };
        }

        [Theory]
        [MemberData(nameof(MockIdExistente))]
        [MemberData(nameof(MockIdInexistente))]
        public async void TesteDeletarProduto(int id)
        {
            InitializeTest();

            try
            {
                var quantidadeRegistrosDeletados = await _produtoService.DeletarProduto(id);
                Assert.Equal(1, quantidadeRegistrosDeletados);
            }
            catch (Exception ex)
            {
                Assert.Equal("Produto não foi encontrado", ex.Message);
            }
        }
    }
}
