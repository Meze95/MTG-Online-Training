using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTG_OnlineTraining.Migrations
{
    public partial class AddUpdatedModelsToDbb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Programs_ProgramsId",
                table: "Materials");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "ProgramDescription",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "ProgramImg",
                table: "Materials");

            migrationBuilder.RenameColumn(
                name: "ProgramsId",
                table: "Materials",
                newName: "AdminProgramId");

            migrationBuilder.RenameColumn(
                name: "ProgramName",
                table: "Materials",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_ProgramsId",
                table: "Materials",
                newName: "IX_Materials_AdminProgramId");

            migrationBuilder.CreateTable(
                name: "StudentPrograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdminProgramId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModeOfTraining = table.Column<int>(type: "int", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentPrograms_AdminProgram_AdminProgramId",
                        column: x => x.AdminProgramId,
                        principalTable: "AdminProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentPrograms_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentPrograms_AdminProgramId",
                table: "StudentPrograms",
                column: "AdminProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPrograms_UserId",
                table: "StudentPrograms",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_AdminProgram_AdminProgramId",
                table: "Materials",
                column: "AdminProgramId",
                principalTable: "AdminProgram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_AdminProgram_AdminProgramId",
                table: "Materials");

            migrationBuilder.DropTable(
                name: "StudentPrograms");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Materials",
                newName: "ProgramName");

            migrationBuilder.RenameColumn(
                name: "AdminProgramId",
                table: "Materials",
                newName: "ProgramsId");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_AdminProgramId",
                table: "Materials",
                newName: "IX_Materials_ProgramsId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Materials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Materials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProgramDescription",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProgramImg",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModeOfTraining = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartinDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Programs_ProgramsId",
                table: "Materials",
                column: "ProgramsId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
