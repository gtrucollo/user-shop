using Domain.Entities.Base;

namespace Domain.Entities;

public class Produto : Entidade
{
    #region Propriedades
    /// <summary>
    /// Obtém ou define Inclusao
    /// </summary>
    public DateTimeOffset Inclusao { get; set; } = DateTimeOffset.Now;

    /// <summary>
    /// Obtém ou define Alteracao
    /// </summary>
    public DateTimeOffset Alteracao { get; set; } = DateTimeOffset.Now;

    /// <summary>
    /// Obtém ou define Nome
    /// </summary>
    public string Nome { get; set; }

    /// <summary>
    /// Obtém ou define Valor
    /// </summary>
    public double Valor { get; set; }
    #endregion

    #region Métodos
    public override void Validar()
    {
        if (string.IsNullOrWhiteSpace(this.Nome))
        {
            throw new Exception("O Nome deve ser preenchido.");
        }

        if (this.Valor <= 0)
        {
            throw new Exception("O Valor deve ser maior que R$ 0,00.");
        }
    }
    #endregion
}