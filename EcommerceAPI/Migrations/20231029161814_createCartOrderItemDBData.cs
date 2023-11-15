using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class createCartOrderItemDBData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CartItems_CartOrderItemId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CartOrderItemId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CartOrderItemId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "CartOrderItemId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CartOrderItemId",
                table: "Products",
                column: "CartOrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CartItems_CartOrderItemId",
                table: "Products",
                column: "CartOrderItemId",
                principalTable: "CartItems",
                principalColumn: "Id");
        }
    }
}
