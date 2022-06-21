using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories
{
    public interface IPedidoVendaRepository : IRepository<PedidoVenda>
    {
        public Task<List<PedidoVenda>> SelecionarTodos();
    }
}