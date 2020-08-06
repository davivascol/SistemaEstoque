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

        // GET: api/Products
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
        {
            return Ok(await _produtoService.ListarProdutos());
        }

        //// GET: api/Products
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ProdutoEntity>>> GetProdutos()
        //{
        //    return await _context.Produtos.ToListAsync();
        //}

        // GET: api/Products/5
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

        // GET: api/Products/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ProdutoEntity>> GetProduto(int id)
        //{
        //    var produto = await _context.Produtos.FindAsync(id);

        //    if (produto == null)
        //    {
        //        return NotFound();
        //    }

        //    return produto;
        //}

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, ProdutoDTO produto)
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

        //// PUT: api/Products/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProduto(int id, ProdutoEntity produto)
        //{
        //    if (id != produto.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(produto).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProdutoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> PostProduto(ProdutoDTO produto)
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

        //// POST: api/Products
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<ProdutoEntity>> PostProduto(ProdutoEntity produto)
        //{
        //    _context.Produtos.Add(produto);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        //}

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

        //// DELETE: api/Products/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<ProdutoEntity>> DeleteProduto(int id)
        //{
        //    var produto = await _context.Produtos.FindAsync(id);
        //    if (produto == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Produtos.Remove(produto);
        //    await _context.SaveChangesAsync();

        //    return produto;
        //}
    }
}
