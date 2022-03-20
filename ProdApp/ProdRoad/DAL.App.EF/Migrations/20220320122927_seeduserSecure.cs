using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.App.EF.Migrations
{
    public partial class seeduserSecure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d86fc1e4-3ae5-4a09-b544-e87f8b7dc0bd", "AQAAAAEAACcQAAAAEO1RlHCKyVtk0frN1xzsb6d+6sVdAv0UPfOsUg0qkZvlB/h2SEWUI0eKW3dvXVzdJg==", "9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fce4feed-43a7-4f40-9c70-c2cf4a846b99", "AQAAAAEAACcQAAAAECXiy8NayHtVTt5NQR+CzeUnlQRUyM0/uB1ED7heLxfZFjJtgaSMFP1cokNmCZmkHw==", "1540a51f-d5d9-45d4-8a85-e7585901d4e6" });
        }
    }
}
