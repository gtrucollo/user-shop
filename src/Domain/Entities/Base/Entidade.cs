namespace Domain.Entities.Base;

public abstract class Entidade
{
    /// <summary>
    /// Obtém ou define Id
    /// </summary>
    public virtual Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Validar os dados
    /// </summary>
    public virtual void Validar()
    {
        return;
    }
}