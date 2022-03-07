using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.App.EF.Migrations
{
    public partial class fixItemProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemProcedures_Processes_ProcessId",
                table: "ItemProcedures");

            migrationBuilder.DropIndex(
                name: "IX_ItemProcedures_ProcessId",
                table: "ItemProcedures");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "ItemProcedures");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProcedureId",
                table: "ItemProcedures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ProcedureId",
                table: "ItemProcedures",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "ProcessId",
                table: "ItemProcedures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ItemProcedures_ProcessId",
                table: "ItemProcedures",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemProcedures_Processes_ProcessId",
                table: "ItemProcedures",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
