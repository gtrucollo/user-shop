using Domain.Entities.Base;

namespace Domain.Entities;

public class PedidoVendaItem : Entidade
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
    /// Obtém ou define IdPedidoVenda
    /// </summary>
    public PedidoVenda IdPedidoVenda { get; set; }

    /// <summary>
    /// Obtém ou define IdProduto
    /// </summary>
    public Produto IdProduto { get; set; }

    /// <summary>
    /// Obtém ou define ValorTotal
    /// </summary>
    public double ValorTotal { get; set; }

    /// <summary>
    /// Obtém ou define Quantidade
    /// </summary>
    public uint Quantidade { get; set; }
    #endregion
}