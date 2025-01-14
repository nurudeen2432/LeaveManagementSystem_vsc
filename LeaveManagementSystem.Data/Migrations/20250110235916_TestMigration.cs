using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4969fc6f-c322-46ab-bf8b-49da83b718b9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "505d3bc5-cacd-4c80-a7f6-09c11355ad97", "AQAAAAIAAYagAAAAECt7xLoSnQ6zy9rO0dpGJzhLWpgiGm6StMoTJeSVhuYW2YJJItBMtpUMA3J6NgkXVw==", "7c80da13-43d1-4adf-ad96-e73aeffe178f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4969fc6f-c322-46ab-bf8b-49da83b718b9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e094d345-6711-4868-96a2-c056c8acbc4c", "AQAAAAIAAYagAAAAEM01DruStejUame8xGdhLn5uyvJSxum6y4YCfC4okG6iJsMdNmyJ7wp/+NuCJDTKpw==", "ec0185bf-c328-4327-b267-3b9b43f925a7" });
        }
    }
}
