using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMesaGestor.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CriacaoData",
                table: "Usuarios",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "statusProPed",
                table: "ProdutoPedido",
                newName: "StatusProPed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Usuarios",
                newName: "CriacaoData");

            migrationBuilder.RenameColumn(
                name: "StatusProPed",
                table: "ProdutoPedido",
                newName: "statusProPed");
        }
    }
}
