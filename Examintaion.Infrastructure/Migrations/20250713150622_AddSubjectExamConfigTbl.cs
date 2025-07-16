using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examintaion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectExamConfigTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConfigId",
                table: "Subjects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SubjectConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOsQuestions = table.Column<short>(type: "smallint", nullable: false),
                    Easy = table.Column<short>(type: "smallint", nullable: false),
                    Miduiem = table.Column<short>(type: "smallint", nullable: false),
                    Hard = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectConfigurations_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectConfigurations_SubjectId",
                table: "SubjectConfigurations",
                column: "SubjectId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectConfigurations");

            migrationBuilder.DropColumn(
                name: "ConfigId",
                table: "Subjects");
        }
    }
}
