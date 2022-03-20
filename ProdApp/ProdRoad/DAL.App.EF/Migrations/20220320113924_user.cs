using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.App.EF.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonalCode", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"), 0, "9825f1e7-da57-4d1c-a21f-f99ad1494d56", "admin@gmail.com", false, null, null, false, null, null, null, null, null, "1234567890", false, null, false, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"));
        }
    }
}
