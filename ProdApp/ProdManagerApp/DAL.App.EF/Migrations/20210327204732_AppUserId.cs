using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class AppUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveNotifications_AspNetUsers_ApplicationUserId",
                table: "ActiveNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionMetas_AspNetUsers_ApplicationUserId",
                table: "ProductionMetas");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_AspNetUsers_ApplicationUserId",
                table: "UserNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_AspNetUsers_ApplicationUserId",
                table: "UserTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_AspNetUsers_ApplicationUserId",
                table: "Warehouses");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Warehouses",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouses_ApplicationUserId",
                table: "Warehouses",
                newName: "IX_Warehouses_AppUserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "UserTeams",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTeams_ApplicationUserId",
                table: "UserTeams",
                newName: "IX_UserTeams_AppUserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "UserNotifications",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserNotifications_ApplicationUserId",
                table: "UserNotifications",
                newName: "IX_UserNotifications_AppUserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ProductionMetas",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionMetas_ApplicationUserId",
                table: "ProductionMetas",
                newName: "IX_ProductionMetas_AppUserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Orders",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders",
                newName: "IX_Orders_AppUserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ActiveNotifications",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ActiveNotifications_ApplicationUserId",
                table: "ActiveNotifications",
                newName: "IX_ActiveNotifications_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveNotifications_AspNetUsers_AppUserId",
                table: "ActiveNotifications",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_AppUserId",
                table: "Orders",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionMetas_AspNetUsers_AppUserId",
                table: "ProductionMetas",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_AspNetUsers_AppUserId",
                table: "UserNotifications",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_AspNetUsers_AppUserId",
                table: "UserTeams",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_AspNetUsers_AppUserId",
                table: "Warehouses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveNotifications_AspNetUsers_AppUserId",
                table: "ActiveNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_AppUserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionMetas_AspNetUsers_AppUserId",
                table: "ProductionMetas");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_AspNetUsers_AppUserId",
                table: "UserNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_AspNetUsers_AppUserId",
                table: "UserTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_AspNetUsers_AppUserId",
                table: "Warehouses");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Warehouses",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouses_AppUserId",
                table: "Warehouses",
                newName: "IX_Warehouses_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "UserTeams",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTeams_AppUserId",
                table: "UserTeams",
                newName: "IX_UserTeams_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "UserNotifications",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserNotifications_AppUserId",
                table: "UserNotifications",
                newName: "IX_UserNotifications_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "ProductionMetas",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionMetas_AppUserId",
                table: "ProductionMetas",
                newName: "IX_ProductionMetas_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Orders",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AppUserId",
                table: "Orders",
                newName: "IX_Orders_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "ActiveNotifications",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ActiveNotifications_AppUserId",
                table: "ActiveNotifications",
                newName: "IX_ActiveNotifications_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveNotifications_AspNetUsers_ApplicationUserId",
                table: "ActiveNotifications",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionMetas_AspNetUsers_ApplicationUserId",
                table: "ProductionMetas",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_AspNetUsers_ApplicationUserId",
                table: "UserNotifications",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_AspNetUsers_ApplicationUserId",
                table: "UserTeams",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_AspNetUsers_ApplicationUserId",
                table: "Warehouses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
