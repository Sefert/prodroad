using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.App.EF.Migrations
{
    public partial class fix_customerprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPrices_Items_ItemId",
                table: "CustomerPrices");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPrices_ItemId",
                table: "CustomerPrices");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "CustomerPrices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "CustomerPrices",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPrices_ItemId",
                table: "CustomerPrices",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPrices_Items_ItemId",
                table: "CustomerPrices",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
