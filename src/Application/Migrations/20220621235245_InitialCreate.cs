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
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Inclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Valor = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
