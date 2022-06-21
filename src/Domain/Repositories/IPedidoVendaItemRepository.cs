using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories
{
    public interface IPedidoVendaItemRepository : IRepository<PedidoVendaItem>
    {
        public Task<List<PedidoVenda>> SelecionarTodosPorPedidoVenda(Guid pedidoVendaId);
    }
}