using Application.ObjetosDto;
using Domain.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Application.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase
{
    readonly IProdutoRepository _produtoRepository;

    public ProdutoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet("SelecionarTodos")]
    public async Task<IActionResult> SelecionarTodos()
    {
        try
        {
            List<Produto> produtos = await _produtoRepository.SelecionarTodosAsync();

            return Ok(produtos.Select(p => new { p.Id, p.Nome, p.Valor }));
        }
        catch (Exception exp)
        {
            return BadRequest(exp.Message);
        }
    }

    [HttpGet("SelecionarPorId/{produtoId}")]
    public async Task<IActionResult> SelecionarPorId(long produtoId)
    {
        try
        {
            Produto produto = await _produtoRepository.SelecionarPorIdAssync(produtoId);
            if(produto == null)
            {
                return NotFound();
            }

            return Ok(new { produto.Id, produto.Nome, produto.Valor });
        }
        catch (Exception exp)
        {
            return BadRequest(exp.Message);
        }
    }

    [HttpPost("Adicionar")]
    public async Task<IActionResult> Adicionar(ProdutoDto produtoDto)
    {
        try
        {
            Produto produto = new() { Nome = produtoDto.Nome, Valor = produtoDto.Valor };

            produto = await _produtoRepository.AdicionarAsync(produto);

            return Ok(JsonSerializer.Serialize(new { produto.Id, produto.Nome, produto.Valor }));
        }
        catch (Exception exp)
        {
            return BadRequest(exp.Message);
        }
    }

    [HttpPost("Atualizar/{produtoId}")]
    public async Task<IActionResult> Atualizar(long produtoId, ProdutoDto produtoDto)
    {
        try
        {
            Produto produto = await _produtoRepository.SelecionarPorIdAssync(produtoId);
            if(produto == null)
            {
                return NotFound();
            }

            produto.Nome = produtoDto.Nome;
            produto.Valor = produtoDto.Valor;
            produto.Alteracao = DateTimeOffset.UtcNow;

            await _produtoRepository.AtualizarAsync(produto);

            return Ok(JsonSerializer.Serialize(new { produto.Id, produto.Nome, produto.Valor }));
        }
        catch (Exception exp)
        {
            return BadRequest(exp.Message);
        }
    }

    [HttpGet("Remover/{produtoId}")]
    public async Task<IActionResult> Remover(long produtoId)
    {
        try
        {
            Produto produto = await _produtoRepository.SelecionarPorIdAssync(produtoId);
            if (produto == null)
            {
                return NotFound();
            }

            await _produtoRepository.RemoverAsync(produto); 

            return Ok();
        }
        catch(Exception exp)
        {
            return BadRequest(exp.Message);
        }
    }
}