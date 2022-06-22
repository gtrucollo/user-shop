using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        public Task<List<Produto>> SelecionarTodosAsync();

        public Task<Produto> AtualizarAsync(Produto entidade);

        public Task RemoverAsync(Produto entidade);
    }
}