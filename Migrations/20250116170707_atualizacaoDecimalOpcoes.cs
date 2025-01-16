using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMesaGestor.Migrations
{
    /// <inheritdoc />
    public partial class atualizacaoDecimalOpcoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Opcoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OpcaoDesc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OpcaoValor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpcaoQuantMax = table.Column<int>(type: "int", nullable: false),
                    CriacaoData = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GrupoOpcoesId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opcoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opcoes_GrupoOpcoes_GrupoOpcoesId",
                        column: x => x.GrupoOpcoesId,
                        principalTable: "GrupoOpcoes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Opcoes_GrupoOpcoesId",
                table: "Opcoes",
                column: "GrupoOpcoesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opcoes");
        }
    }
}
