using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PedidoVendaRepository : IPedidoVendaRepository
    {
        private readonly DatabaseContext _context;

        public PedidoVendaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<PedidoVenda> AdicionarAsync(PedidoVenda pedidoVenda)
        {
            pedidoVenda.Validar();

            await _context.PedidoVendas.AddAsync(pedidoVenda);

            await _context.SaveChangesAsync();

            return pedidoVenda;
        }

        public async Task<PedidoVenda> SelecionarPorIdAssync(long id)
        {
            return await _context.PedidoVendas.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<PedidoVenda>> SelecionarTodos()
        {
            return await _context.PedidoVendas.ToListAsync();
        }
    }
}