using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTG_OnlineTraining.Migrations
{
    public partial class AddIsActiveInStudentProgramReg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "StudentPrograms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "StudentPrograms");
        }
    }
}
