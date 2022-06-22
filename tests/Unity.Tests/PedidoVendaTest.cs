using Domain.Entities;

namespace Unity.Tests;

public class PedidoVendaTest
{
    private readonly PedidoVenda _pedidoVenda;

    public PedidoVendaTest()
    {
        _pedidoVenda = new PedidoVenda();
    }

    [Fact]
    public void TesteDeveriaEstarRetornandoPedidoVendaZerado()
    {
        Assert.Equal(0, _pedidoVenda.ValorTotal);
    }

    [Fact]
    public void TesteDeveriaEstarRetornandoPedidoVendaComUmItem()
    {
        _pedidoVenda.AdicionarItem(new Produto() { Valor = 19.99 }, 1);

        Assert.Single(_pedidoVenda.Items);
        Assert.Equal(19.99, _pedidoVenda.ValorTotal);
    }

    [Fact]
    public void TesteDeveriaEstarRetornandoPedidoVendaComUmItemEQuantidadeMaior()
    {
        _pedidoVenda.AdicionarItem(new Produto() { Id = 1,  Valor = 19.99 }, 1);
        _pedidoVenda.AdicionarItem(new Produto() { Id = 1, Valor = 19.99 }, 1);

        Assert.Single(_pedidoVenda.Items);

        Assert.Equal((uint)2, _pedidoVenda.Quantidade);

        Assert.Equal(39.98, _pedidoVenda.ValorTotal);
    }

    [Fact]
    public void TesteDeveriaEstarRetornandoPedidoVendaSemItems()
    {
        _pedidoVenda.AdicionarItem(new Produto() { Id = 1, Valor = 19.99 }, 1);
        _pedidoVenda.AdicionarItem(new Produto() { Id = 2, Valor = 19.99 }, 1);

        Assert.Equal(2, _pedidoVenda.Items.Count);

        _pedidoVenda.RemoverItem(new Produto() { Id = 1, Valor = 19.99 }, 1);

        Assert.Single(_pedidoVenda.Items);

        _pedidoVenda.RemoverItem(new Produto() { Id = 2, Valor = 19.99 }, 1);

        Assert.Empty(_pedidoVenda.Items);
    }
}