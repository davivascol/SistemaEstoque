using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EstoqueApi.Repository;
using EstoqueApi.Domain.Entities;
using EstoqueApi.Service;
using EstoqueApi.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace EstoqueApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }
                
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
        {
            return Ok(await _produtoService.ListarProdutos());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDTO>> GetProduto(int id)
        {
            try
            {
                var produto = await _produtoService.RecuperarProduto(id);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, [FromBody] ProdutoDTO produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _produtoService.EditarProduto(produto);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> PostProduto([FromBody] ProdutoDTO produto)
        {
            try 
            {
                await _produtoService.CriarProduto(produto);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoDTO>> DeleteProduto(int id)
        {
            try
            {
                await _produtoService.DeletarProduto(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
