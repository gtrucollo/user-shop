using Application.ObjetosDto;
using Domain.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Linq;

namespace Application.Controllers;

[ApiController]
[Route("[controller]")]
public class PedidoVendaController : ControllerBase
{
    readonly IPedidoVendaRepository _pedidoVendaRepository;

    readonly IProdutoRepository _produtoRepository;

    public PedidoVendaController(IPedidoVendaRepository pedidoVendaRepository, IProdutoRepository produtoRepository)
    {
        _pedidoVendaRepository = pedidoVendaRepository;
        _produtoRepository = produtoRepository;
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
            PedidoVenda pedidoVenda = new() { Quantidade = pedidoVendaDto.Quantidade, ValorTotal =  pedidoVendaDto.ValorTotal };

            var produtosLookup = (await _produtoRepository.SelecionarTodosAsync()).ToLookup(x => x.Id);

            pedidoVendaDto.Items.ForEach(i => pedidoVenda.AdicionarItem(produtosLookup[i.IdProduto].FirstOrDefault(), i.Quantidade));

            pedidoVenda = await _pedidoVendaRepository.AdicionarAsync(pedidoVenda);

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

    [HttpPost("Atualizar/{pedidoVendaId}")]
    public async Task<IActionResult> Atualizar(long pedidoVendaId, PedidoVendaDto pedidoVendaDto)
    {
        try
        {
            try
            {
                PedidoVenda pedidoVenda = await _pedidoVendaRepository.SelecionarPorIdAssync(pedidoVendaId);
                pedidoVenda.Quantidade = pedidoVendaDto.Quantidade;
                pedidoVenda.ValorTotal = pedidoVendaDto.ValorTotal;

                pedidoVenda.Items.ForEach( i => pedidoVenda.RemoverItem(i.IdProduto, i.Quantidade));

                pedidoVendaDto.Items.ForEach(async i => pedidoVenda.AdicionarItem(await _produtoRepository.SelecionarPorIdAssync(i.IdProduto), i.Quantidade));

                pedidoVenda = await _pedidoVendaRepository.AdicionarAsync(pedidoVenda);

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

            return Ok();
        }
        catch (Exception exp)
        {
            return BadRequest(exp.Message);
        }
    }
}