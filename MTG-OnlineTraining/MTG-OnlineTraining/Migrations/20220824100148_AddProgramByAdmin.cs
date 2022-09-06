using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTG_OnlineTraining.Migrations
{
    public partial class AddProgramByAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Programs",
                newName: "ProgramName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Materials",
                newName: "ProgramName");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Programs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModeOfTraining",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProgramDescription",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProgramImg",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartinDate",
                table: "Programs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FlowOrder",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                name: "AdminProgram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminProgram", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminProgram");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ModeOfTraining",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ProgramDescription",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ProgramImg",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "StartinDate",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "File",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "FlowOrder",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "ProgramDescription",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "ProgramImg",
                table: "Materials");

            migrationBuilder.RenameColumn(
                name: "ProgramName",
                table: "Programs",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ProgramName",
                table: "Materials",
                newName: "Name");
        }
    }
}
