using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories
{
    public interface IPedidoVendaRepository : IRepository<PedidoVenda>
    {
        public Task<List<PedidoVenda>> SelecionarTodosAsync();

        public Task<PedidoVenda> AtualizarAsync(PedidoVenda entidade);

        public Task RemoverAsync(PedidoVenda entidade);
    }
}