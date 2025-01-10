using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4969fc6f-c322-46ab-bf8b-49da83b718b9",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2108ec01-744b-40a1-b24a-083fe7d70f8a", new DateOnly(1964, 12, 1), "Default", "admin", "AQAAAAIAAYagAAAAELCNs3I7i8HV5xRICWAZf0fbGJG4YdGk8PgoeBRV22QteZAhyw/u6Bs7ThBQw/ahtg==", "c7b35388-52d7-4bcf-bbc8-bf3269d3010a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4969fc6f-c322-46ab-bf8b-49da83b718b9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd8d0ae1-d792-4396-8786-b9f42fee016b", "AQAAAAIAAYagAAAAEJqlv7iSQcQSSKss9o5mI7QSl7qUPDIECBeI80Nt2GOPXLFOc7PbB7S+fFvmrbkhJA==", "454487ab-2e02-4dfd-9754-342382545da4" });
        }
    }
}
