using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class CriandoUmaRelacaoDeDados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarrinhoId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CarrinhoId",
                table: "Pedidos",
                column: "CarrinhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Carrinho_CarrinhoId",
                table: "Pedidos",
                column: "CarrinhoId",
                principalTable: "Carrinho",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Carrinho_CarrinhoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_CarrinhoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "CarrinhoId",
                table: "Pedidos");
        }
    }
}
