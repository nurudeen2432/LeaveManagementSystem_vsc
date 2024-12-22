using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesandUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6010b61c-2a36-4ef7-a446-a306e25c175f", null, "Employee", "EMPLOYEE" },
                    { "8fe08bf0-d35f-4ee2-828e-b07041a26940", null, "Supervisor", "SUPERVISOR" },
                    { "e8b48d34-ea1b-4cfc-82c5-fbebb938b92e", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4969fc6f-c322-46ab-bf8b-49da83b718b9", 0, "bd8d0ae1-d792-4396-8786-b9f42fee016b", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", null, "AQAAAAIAAYagAAAAEJqlv7iSQcQSSKss9o5mI7QSl7qUPDIECBeI80Nt2GOPXLFOc7PbB7S+fFvmrbkhJA==", null, false, "454487ab-2e02-4dfd-9754-342382545da4", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e8b48d34-ea1b-4cfc-82c5-fbebb938b92e", "4969fc6f-c322-46ab-bf8b-49da83b718b9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6010b61c-2a36-4ef7-a446-a306e25c175f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fe08bf0-d35f-4ee2-828e-b07041a26940");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e8b48d34-ea1b-4cfc-82c5-fbebb938b92e", "4969fc6f-c322-46ab-bf8b-49da83b718b9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8b48d34-ea1b-4cfc-82c5-fbebb938b92e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4969fc6f-c322-46ab-bf8b-49da83b718b9");
        }
    }
}
