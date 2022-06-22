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

            PedidoVenda pedido = new () { Quantidade = pedidoVenda.Quantidade, ValorTotal = pedidoVenda.Quantidade };

            await _context.PedidoVendas.AddAsync(pedido);


            await _context.SaveChangesAsync();

            foreach(PedidoVendaItem item in pedidoVenda.Items)
            {
                item.IdPedidoVenda = pedido;

                _context.PedidoVendasItems.Add(item);
            }

            pedidoVenda.Id = pedido.Id;

            await _context.SaveChangesAsync();

            return pedidoVenda;
        }

        public async Task<PedidoVenda> SelecionarPorIdAssync(long id)
        {
            PedidoVenda pedidoVenda =  await _context.PedidoVendas.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(pedidoVenda == null)
            {
                return null;
            }

            pedidoVenda.Items.Clear();
            pedidoVenda.Items.AddRange(await _context.PedidoVendasItems.Where(x => x.IdPedidoVenda.Id == pedidoVenda.Id).Include(e => e.IdProduto).ToListAsync());

            return pedidoVenda;
        }

        public async Task<List<PedidoVenda>> SelecionarTodosAsync()
        {
            List<PedidoVenda> listaPedidoVenda =  await _context.PedidoVendas.ToListAsync();

            var pedidosVendaItemLookup = (await _context.PedidoVendasItems.Include(e => e.IdProduto).ToListAsync()).ToLookup(x => x.IdPedidoVenda.Id);
            foreach (PedidoVenda pedidoVenda in listaPedidoVenda)
            {
                pedidoVenda.Items.Clear();
                pedidoVenda.Items.AddRange(pedidosVendaItemLookup[pedidoVenda.Id].ToList());
            }

            return listaPedidoVenda;
        }

        public async Task<PedidoVenda> AtualizarAsync(PedidoVenda pedidoVenda)
        {

            pedidoVenda.Validar();

            _context.PedidoVendas.Update(pedidoVenda);

            foreach (PedidoVendaItem item in pedidoVenda.Items)
            {
                item.IdPedidoVenda = pedidoVenda;

                _context.PedidoVendasItems.Add(item);
            }

            await _context.SaveChangesAsync();

            return pedidoVenda;
        }

        public async Task RemoverAsync(PedidoVenda pedidoVenda)
        {
            pedidoVenda.Validar();

            _context.PedidoVendas.Remove(pedidoVenda);

            await _context.SaveChangesAsync();
        }
    }
}