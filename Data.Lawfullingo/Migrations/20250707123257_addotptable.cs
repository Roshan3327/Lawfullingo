using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Lawfullingo.Migrations
{
    /// <inheritdoc />
    public partial class addotptable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_TeacherType_teacherTypeId",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherType",
                table: "TeacherType");

            migrationBuilder.RenameTable(
                name: "TeacherType",
                newName: "TeacherTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherTypes",
                table: "TeacherTypes",
                column: "id");

            migrationBuilder.CreateTable(
                name: "OTPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Otp = table.Column<int>(type: "int", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false),
                    ForOtp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OTPs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OTPs_UserId",
                table: "OTPs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_TeacherTypes_teacherTypeId",
                table: "Teachers",
                column: "teacherTypeId",
                principalTable: "TeacherTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_TeacherTypes_teacherTypeId",
                table: "Teachers");

            migrationBuilder.DropTable(
                name: "OTPs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherTypes",
                table: "TeacherTypes");

            migrationBuilder.RenameTable(
                name: "TeacherTypes",
                newName: "TeacherType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherType",
                table: "TeacherType",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_TeacherType_teacherTypeId",
                table: "Teachers",
                column: "teacherTypeId",
                principalTable: "TeacherType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
