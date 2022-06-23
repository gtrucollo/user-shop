using UI.ObjetosDto;
using UI.Services.Base;

namespace UI.Services;
public class PedidoVendaService : ServiceBase
{
    private const string serviceName = "PedidoVenda";

    public PedidoVendaService(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public async Task<List<PedidoVendaDto>> SelecionarTodos()
    {
        return await this.EfetuarRequisicaoGetAsync<List<PedidoVendaDto>>($"{serviceName}/SelecionarTodos");
    }

    public async Task<PedidoVendaDto> SelecionarPorId(long id)
    {
        return await this.EfetuarRequisicaoGetAsync<PedidoVendaDto>($"{serviceName}/SelecionarPorId/{id}");
    }

    public async Task<PedidoVendaDto> Adicionar(PedidoVendaDto pedidoVendaDto)
    {
        return await this.EfetuarRequisicaoPostAsync<PedidoVendaDto>($"{serviceName}/Adicionar/", pedidoVendaDto);
    }

    public async Task<PedidoVendaDto> Atualizar(long id, PedidoVendaDto pedidoVendaDto)
    {
        return await this.EfetuarRequisicaoPostAsync<PedidoVendaDto>($"{serviceName}/Atualizar/{id}", pedidoVendaDto);
    }

    public async Task Remover(long id)
    {
        await this.EfetuarRequisicaoGetAsync($"{serviceName}/Remover/{id}");
    }
}
