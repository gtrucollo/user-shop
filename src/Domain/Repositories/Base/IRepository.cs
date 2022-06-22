using Domain.Entities.Base;

namespace Domain.Repositories.Base;

public interface IRepository<TEntity> where TEntity : Entidade
{
    Task<TEntity> AdicionarAsync(TEntity entidade);

    Task<TEntity> SelecionarPorIdAssync(long id);
}