using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.App.EF.Migrations
{
    public partial class seeduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "UserName" },
                values: new object[] { "2069c366-8c83-48e7-9bb7-3bb9af04ffb2", "AQAAAAEAACcQAAAAEOtwq7XEZ9mSInFfazGRgmGOQIuc5kzV2HAG+iOQeA8apFNdteGmCLIePolrXUZPdg==", "admin@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "UserName" },
                values: new object[] { "9825f1e7-da57-4d1c-a21f-f99ad1494d56", null, "Admin" });
        }
    }
}
