using System.Linq;
using System.Text.Json;
using Application.ObjetosDto;
using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("[controller]")]
public class PedidoVendaController : ControllerBase
{
    readonly IPedidoVendaRepository _pedidoVendaRepository;

    readonly IProdutoRepository _produtoRepository;

    readonly FilaPedidoVenda _filaPedidoVenda;

    public PedidoVendaController(
        IPedidoVendaRepository pedidoVendaRepository,
        IProdutoRepository produtoRepository,
        FilaPedidoVenda filaPedidoVenda)
    {
        _pedidoVendaRepository = pedidoVendaRepository;
        _produtoRepository = produtoRepository;
        _filaPedidoVenda = filaPedidoVenda;
    }

    [HttpGet("SelecionarTodos")]
    public async Task<IActionResult> SelecionarTodos()
    {
        try
        {
            List<PedidoVenda> pedidosVenda = await _pedidoVendaRepository.SelecionarTodosAsync();

            return Ok(JsonSerializer.Serialize(
                pedidosVenda.Select(p =>
                new
                {
                    p.Id,
                    p.Quantidade,
                    p.ValorTotal,
                    Items = p.Items.Select(y => new { y.IdProduto.Id, y.Quantidade, y.ValorTotal, })
                })));
        }
        catch (Exception exp)
        {
            return BadRequest(exp.Message);
        }
    }

    [HttpGet("SelecionarPorId/{pedidoVendaId}")]
    public async Task<IActionResult> SelecionarPorId(long pedidoVendaId)
    {
        try
        {
            PedidoVenda pedidoVenda = await _pedidoVendaRepository.SelecionarPorIdAssync(pedidoVendaId);
            if (pedidoVenda == null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(
               new
               {
                   pedidoVenda.Id,
                   pedidoVenda.Quantidade,
                   pedidoVenda.ValorTotal,
                   Items = pedidoVenda.Items.Select(y => new { y.IdProduto.Id, y.Quantidade, y.ValorTotal, })
               }));
        }
        catch (Exception exp)
        {
            return BadRequest(exp.Message);
        }
    }

    [HttpPost("Adicionar")]
    public async Task<IActionResult> Adicionar(PedidoVendaDto pedidoVendaDto)
    {
        try
        {
            await _filaPedidoVenda.EnviarPedidoVendaFilaAsync(pedidoVendaDto);

            return Ok(JsonSerializer.Serialize(pedidoVendaDto));
        }
        catch (Exception exp)
        {
            return BadRequest(exp.Message);
        }
    }

    [HttpPost("Atualizar/{pedidoVendaId}")]
    public async Task<IActionResult> Atualizar(long pedidoVendaId, PedidoVendaDto pedidoVendaDto)
    {
        try
        {
            pedidoVendaDto.Id = pedidoVendaId;

            await _filaPedidoVenda.EnviarPedidoVendaFilaAsync(pedidoVendaDto);

            return Ok(JsonSerializer.Serialize(pedidoVendaDto));
        }
        catch (Exception exp)
        {
            return BadRequest(exp.Message);
        }
    }

    [HttpGet("Remover/{pedidoVendaId}")]
    public async Task<IActionResult> Remover(long pedidoVendaId)
    {
        try
        {
            PedidoVenda pedidoVenda = await _pedidoVendaRepository.SelecionarPorIdAssync(pedidoVendaId);
            if (pedidoVenda == null)
            {
                return NotFound();
            }

            await _pedidoVendaRepository.RemoverAsync(pedidoVenda);

            return NoContent();
        }
        catch (Exception exp)
        {
            return BadRequest(exp.Message);
        }
    }
}