﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class addProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Products");
        }
    }
}
