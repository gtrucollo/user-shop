using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class DatabaseContext : DbContext
{
    /// <summary>
    /// Inicia uma nova instância da classe <seealso cref="DatabaseContext"/>
    /// </summary>
    /// <param name="options">Parâmetros</param>
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }

    public DbSet<PedidoVenda> PedidoVendas { get; set; }

    public DbSet<PedidoVendaItem> PedidoVendasItems { get; set; }
}