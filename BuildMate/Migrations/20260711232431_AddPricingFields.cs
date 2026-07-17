using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildMate.Migrations
{
    /// <inheritdoc />
    public partial class AddPricingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BulkDiscountsJson",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveFrom",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceList",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BulkDiscountsJson",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EffectiveFrom",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PriceList",
                table: "Products");
        }
    }
}
