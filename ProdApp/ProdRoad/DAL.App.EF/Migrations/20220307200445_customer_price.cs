using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.App.EF.Migrations
{
    public partial class customer_price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPrice_AspNetUsers_CreatedById",
                table: "CustomerPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPrice_AspNetUsers_UpdatedById",
                table: "CustomerPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPrice_CustomerPriceGroups_CustomerPriceGroupId",
                table: "CustomerPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPrice_Items_ItemId",
                table: "CustomerPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPrice_Prices_PriceId",
                table: "CustomerPrice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerPrice",
                table: "CustomerPrice");

            migrationBuilder.RenameTable(
                name: "CustomerPrice",
                newName: "CustomerPrices");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerPrice_UpdatedById",
                table: "CustomerPrices",
                newName: "IX_CustomerPrices_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerPrice_PriceId",
                table: "CustomerPrices",
                newName: "IX_CustomerPrices_PriceId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerPrice_ItemId",
                table: "CustomerPrices",
                newName: "IX_CustomerPrices_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerPrice_CustomerPriceGroupId",
                table: "CustomerPrices",
                newName: "IX_CustomerPrices_CustomerPriceGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerPrice_CreatedById",
                table: "CustomerPrices",
                newName: "IX_CustomerPrices_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerPrices",
                table: "CustomerPrices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPrices_AspNetUsers_CreatedById",
                table: "CustomerPrices",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPrices_AspNetUsers_UpdatedById",
                table: "CustomerPrices",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPrices_CustomerPriceGroups_CustomerPriceGroupId",
                table: "CustomerPrices",
                column: "CustomerPriceGroupId",
                principalTable: "CustomerPriceGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPrices_Items_ItemId",
                table: "CustomerPrices",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPrices_Prices_PriceId",
                table: "CustomerPrices",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPrices_AspNetUsers_CreatedById",
                table: "CustomerPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPrices_AspNetUsers_UpdatedById",
                table: "CustomerPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPrices_CustomerPriceGroups_CustomerPriceGroupId",
                table: "CustomerPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPrices_Items_ItemId",
                table: "CustomerPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPrices_Prices_PriceId",
                table: "CustomerPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerPrices",
                table: "CustomerPrices");

            migrationBuilder.RenameTable(
                name: "CustomerPrices",
                newName: "CustomerPrice");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerPrices_UpdatedById",
                table: "CustomerPrice",
                newName: "IX_CustomerPrice_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerPrices_PriceId",
                table: "CustomerPrice",
                newName: "IX_CustomerPrice_PriceId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerPrices_ItemId",
                table: "CustomerPrice",
                newName: "IX_CustomerPrice_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerPrices_CustomerPriceGroupId",
                table: "CustomerPrice",
                newName: "IX_CustomerPrice_CustomerPriceGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerPrices_CreatedById",
                table: "CustomerPrice",
                newName: "IX_CustomerPrice_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerPrice",
                table: "CustomerPrice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPrice_AspNetUsers_CreatedById",
                table: "CustomerPrice",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPrice_AspNetUsers_UpdatedById",
                table: "CustomerPrice",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPrice_CustomerPriceGroups_CustomerPriceGroupId",
                table: "CustomerPrice",
                column: "CustomerPriceGroupId",
                principalTable: "CustomerPriceGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPrice_Items_ItemId",
                table: "CustomerPrice",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPrice_Prices_PriceId",
                table: "CustomerPrice",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
