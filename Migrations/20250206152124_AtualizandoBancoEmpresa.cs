using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMesaGestor.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoBancoEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CriacaoData",
                table: "Empresas",
                newName: "DataCriacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Empresas",
                newName: "CriacaoData");
        }
    }
}
