using System.Text.Json;
using Application.ObjetosDto;
using Azure.Messaging.ServiceBus;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories;

namespace Application.Services;

public class FilaPedidoVendaWorker : IHostedService
{
    private readonly ILogger<FilaPedidoVendaWorker> _logger;

    private readonly ServiceBusProcessor _processor;

    private readonly IServiceScopeFactory _scopeFactor;

    public FilaPedidoVendaWorker(
        ILogger<FilaPedidoVendaWorker> logger,
        ServiceBusClient serviceBusClient,
        IServiceScopeFactory scopeFactor)
    {
        _logger = logger;
        _processor = serviceBusClient.CreateProcessor("pedido_venda_fila");
        _scopeFactor = scopeFactor;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _processor.ProcessMessageAsync += this.ProcessarMensagemAsync;
        _processor.ProcessErrorAsync += this.ProcessarMensagemErroAsync;


        await _processor.StartProcessingAsync(cancellationToken);
    }

    public async Task ProcessarMensagemAsync(ProcessMessageEventArgs messageEventArgs)
    {
        using (var scope = _scopeFactor.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            PedidoVendaRepository pedidoVendaRepository = new PedidoVendaRepository(dbContext);
            ProdutoRepository produtoRepository = new ProdutoRepository(dbContext);

            PedidoVendaDto pedidoVendaDto = JsonSerializer.Deserialize<PedidoVendaDto>(messageEventArgs.Message.Body.ToString());

            PedidoVenda pedidoVenda;
            switch (pedidoVendaDto.Id > 0)
            {
                case true:
                    pedidoVenda = await pedidoVendaRepository.SelecionarPorIdAssync(pedidoVendaDto.Id);
                    break;

                default:
                    pedidoVenda = new();
                    break;
            }

            pedidoVenda.Quantidade = pedidoVendaDto.Quantidade;
            pedidoVenda.ValorTotal = pedidoVendaDto.ValorTotal;
            pedidoVenda.Items.ToList().ForEach(i => pedidoVenda.RemoverItem(i.IdProduto, i.Quantidade));

            var produtosLookup = (await produtoRepository.SelecionarTodosAsync()).ToLookup(x => x.Id);

            pedidoVendaDto.Items.ForEach(i => pedidoVenda.AdicionarItem(produtosLookup[i.IdProduto].FirstOrDefault(), i.Quantidade));
            switch (pedidoVendaDto.Id > 0)
            {
                case true:
                    await pedidoVendaRepository.AtualizarAsync(pedidoVenda);
                    break;

                default:
                    await pedidoVendaRepository.AdicionarAsync(pedidoVenda);
                    break;
            }
        }
    }

    public Task ProcessarMensagemErroAsync(ProcessErrorEventArgs errorEventArgs)
    {
        _logger.LogError(errorEventArgs.Exception, "Erro ao consumir os dados da fila de pedidos de venda");

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _processor.ProcessMessageAsync -= this.ProcessarMensagemAsync;
        _processor.ProcessErrorAsync -= this.ProcessarMensagemErroAsync;

        await _processor.CloseAsync(cancellationToken);
    }
}