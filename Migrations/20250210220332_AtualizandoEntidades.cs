using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMesaGestor.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CriacaoData",
                table: "Transacoes",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "CriacaoData",
                table: "Setores",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "CriacaoData",
                table: "Produtos",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "CriacaoData",
                table: "ProdutoPedido",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "CriacaoData",
                table: "Pedidos",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "CriacaoData",
                table: "Opcoes",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "CriacaoData",
                table: "OpcaoProPed",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "CriacaoData",
                table: "Mesas",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "CriacaoData",
                table: "GrupoOpcoes",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "CriacaoData",
                table: "Categorias",
                newName: "DataCriacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Transacoes",
                newName: "CriacaoData");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Setores",
                newName: "CriacaoData");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Produtos",
                newName: "CriacaoData");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "ProdutoPedido",
                newName: "CriacaoData");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Pedidos",
                newName: "CriacaoData");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Opcoes",
                newName: "CriacaoData");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "OpcaoProPed",
                newName: "CriacaoData");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Mesas",
                newName: "CriacaoData");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "GrupoOpcoes",
                newName: "CriacaoData");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Categorias",
                newName: "CriacaoData");
        }
    }
}
