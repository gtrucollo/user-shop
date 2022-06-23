using Domain.Entities.Base;

namespace Domain.Entities;

public class PedidoVenda : Entidade
{
    #region Propriedades
    /// <summary>
    /// Obtém ou define Inclusao
    /// </summary>
    public DateTimeOffset Inclusao { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Obtém ou define Alteracao
    /// </summary>
    public DateTimeOffset Alteracao { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Obtém ou define ValorTotal
    /// </summary>
    public double ValorTotal { get; set; }

    /// <summary>
    /// Obtém ou define Quantidade
    /// </summary>
    public uint Quantidade { get; set; }

    /// <summary>
    /// Obtém ou define Items
    /// </summary>
    public List<PedidoVendaItem> Items { get; set; } = new();
    #endregion

    #region Métodos
    /// <summary>
    /// Adicionar um novo item
    /// </summary>
    /// <param name="produto">Dados do produto</param>
    /// <param name="quantidade">A quantidade a ser adquirida</param>
    public void AdicionarItem(Produto produto, uint quantidade)
    {
        PedidoVendaItem item = this.Items.Where(x => x.IdProduto?.Id == produto.Id).FirstOrDefault();
        if (item == null)
        {
            item = new() { IdProduto = produto };
            this.Items.Add(item);
        }

        item.Quantidade += quantidade;
        item.ValorTotal = Math.Round(produto.Valor * item.Quantidade, 2, MidpointRounding.AwayFromZero);

        this.AtualizarValores();
    }

    /// <summary>
    /// Adicionar um novo item
    /// </summary>
    /// <param name="produto">Dados do produto</param>
    /// <param name="quantidade">A quantidade a ser adquirida</param>
    public void RemoverItem(Produto produto, uint quantidade)
    {
        PedidoVendaItem item = this.Items.Where(x => x.IdProduto?.Id == produto.Id).FirstOrDefault();
        if (item == null)
        {
            return;
        }

        item.Quantidade -= quantidade;
        if (item.Quantidade <= 0)
        {
            this.Items.Remove(item);
            this.AtualizarValores();
            return;
        }

        item.ValorTotal = Math.Round(item.ValorTotal - produto.Valor, 2, MidpointRounding.AwayFromZero);
        this.AtualizarValores();
    }

    private void AtualizarValores()
    {
        this.Quantidade = (uint)this.Items.Sum(x => x.Quantidade);

        this.ValorTotal = Math.Round(this.Items.Sum(x => x.ValorTotal), 2, MidpointRounding.AwayFromZero);
    }
    #endregion
}