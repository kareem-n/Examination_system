using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examintaion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifiyRoleNromalizedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0ce1f56-6f05-464f-9dfb-fa705437eb20",
                column: "NormalizedName",
                value: "STUDENT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0ce1f56-6f05-464f-9dfb-fa705437eb20",
                column: "NormalizedName",
                value: "CUSTOMER");
        }
    }
}
