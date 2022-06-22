using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Application.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidoVendas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Inclusao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Alteracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ValorTotal = table.Column<double>(type: "double precision", nullable: false),
                    Quantidade = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoVendas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Inclusao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Alteracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Valor = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidoVendasItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Inclusao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Alteracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IdPedidoVendaId = table.Column<long>(type: "bigint", nullable: true),
                    IdProdutoId = table.Column<long>(type: "bigint", nullable: true),
                    ValorTotal = table.Column<double>(type: "double precision", nullable: false),
                    Quantidade = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoVendasItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoVendasItems_PedidoVendas_IdPedidoVendaId",
                        column: x => x.IdPedidoVendaId,
                        principalTable: "PedidoVendas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PedidoVendasItems_Produtos_IdProdutoId",
                        column: x => x.IdProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoVendasItems_IdPedidoVendaId",
                table: "PedidoVendasItems",
                column: "IdPedidoVendaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoVendasItems_IdProdutoId",
                table: "PedidoVendasItems",
                column: "IdProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoVendasItems");

            migrationBuilder.DropTable(
                name: "PedidoVendas");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
