using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoATabelacarrinho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdutoID",
                table: "Carrinho");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Carrinho");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdutoID",
                table: "Carrinho",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioID",
                table: "Carrinho",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
