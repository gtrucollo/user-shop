using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProdutoRepository : DbContext, IProdutoRepository
    {
        /// <summary>
        /// Inicia uma nova instância da classe <seealso cref="ProdutoRepository"/>
        /// </summary>
        /// <param name="options">Parâmetros</param>
        public ProdutoRepository(DbContextOptions<ProdutoRepository> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }

        public async Task<Produto> AdicionarAsync(Produto produto)
        {
            produto.Validar();

            await this.AddAsync(produto);

            await this.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> SelecionarPorIdAssync(long id)
        {
            return await this.Produtos.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}