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
    IProdutoRepository _produtoRepository;

    public ProdutoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpPost("Adicionar")]
    public async Task<IActionResult> Adicionar(ProdutoCadastroDto produtoDto)
    {
        try
        {
            Produto produto = new() { Nome = produtoDto.Nome, Valor = produtoDto.Valor };

            produto = await _produtoRepository.AdicionarAsync(produto);

            return Ok(JsonSerializer.Serialize(new { produto.Id, produto.Nome, produto.Valor }));
        }
        catch
        {
            return BadRequest("Ocorreu um erro ao processar os dados.");
        }
    }
}