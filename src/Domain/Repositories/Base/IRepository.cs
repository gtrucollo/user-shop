using Domain.Entities.Base;

namespace Domain.Repositories.Base;

public interface IRepository<TEntity> where TEntity : Entidade
{
    /// <summary>
    /// Salvar um registro
    /// </summary>
    /// <param name="entidade">Os dados da entidade a ser salva</param>
    /// <returns>A entidade salva</returns>
    Task<TEntity> SalvarAsync(TEntity entidade);

    /// <summary>
    /// Selecionar por identificador
    /// </summary>
    /// <param name="id">Identificador da entidade</param>
    /// <returns>Os dados da entidade</returns>
    Task<TEntity> SelecionarPorIdAssync(Guid id);
}