using System.Text.Json;
using Application.ObjetosDto;
using Azure.Messaging.ServiceBus;
namespace Application.Services;

public class FilaPedidoVenda
{
    private readonly ServiceBusSender _sender;
    public FilaPedidoVenda(ServiceBusClient serviceBusClient)
    {
        _sender = serviceBusClient.CreateSender("pedido_venda_fila");
    }

    public async Task EnviarPedidoVendaFilaAsync(PedidoVendaDto pedidoVendaDto)
    {
        await _sender.SendMessageAsync(new ServiceBusMessage(JsonSerializer.Serialize((pedidoVendaDto))));
    }
}