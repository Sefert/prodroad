using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.App.EF.Migrations
{
    public partial class seeduserFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"),
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp" },
                values: new object[] { "fce4feed-43a7-4f40-9c70-c2cf4a846b99", true, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAECXiy8NayHtVTt5NQR+CzeUnlQRUyM0/uB1ED7heLxfZFjJtgaSMFP1cokNmCZmkHw==", true, "1540a51f-d5d9-45d4-8a85-e7585901d4e6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"),
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp" },
                values: new object[] { "2069c366-8c83-48e7-9bb7-3bb9af04ffb2", false, null, null, "AQAAAAEAACcQAAAAEOtwq7XEZ9mSInFfazGRgmGOQIuc5kzV2HAG+iOQeA8apFNdteGmCLIePolrXUZPdg==", false, null });
        }
    }
}
