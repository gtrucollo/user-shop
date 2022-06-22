using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DatabaseContext _context;

        /// <summary>
        /// Inicia uma nova instância da classe <seealso cref="ProdutoRepository"/>
        /// </summary>
        /// <param name="options">Parâmetros</param>
        public ProdutoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Produto> AdicionarAsync(Produto produto)
        {
            produto.Validar();

            await _context.Produtos.AddAsync(produto);

            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> AtualizarAsync(Produto produto)
        {
            produto.Validar();

            _context.Produtos.Update(produto);

            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task RemoverAsync(Produto produto)
        {
            produto.Validar();

            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();
        }

        public async Task<Produto> SelecionarPorIdAssync(long id)
        {
            return await _context.Produtos.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Produto>> SelecionarTodosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }
    }
}