using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoAsColunas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeProduto",
                table: "Carrinho",
                newName: "UsuarioNome");

            migrationBuilder.RenameColumn(
                name: "CarrinhoID",
                table: "Carrinho",
                newName: "ID");

            migrationBuilder.AddColumn<string>(
                name: "ProdutoNome",
                table: "Carrinho",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "ProdutoPreco",
                table: "Carrinho",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdutoNome",
                table: "Carrinho");

            migrationBuilder.DropColumn(
                name: "ProdutoPreco",
                table: "Carrinho");

            migrationBuilder.RenameColumn(
                name: "UsuarioNome",
                table: "Carrinho",
                newName: "NomeProduto");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Carrinho",
                newName: "CarrinhoID");
        }
    }
}
