using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Base;

public abstract class Entidade
{
    /// <summary>
    /// Obtém ou define Id
    /// </summary>
    [Key]
    public virtual long Id { get; set; }

    /// <summary>
    /// Validar os dados
    /// </summary>
    public virtual void Validar()
    {
        return;
    }
}