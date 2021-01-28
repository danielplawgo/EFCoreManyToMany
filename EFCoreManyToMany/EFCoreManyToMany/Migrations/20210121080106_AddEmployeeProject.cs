using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreManyToMany.Migrations
{
    public partial class AddEmployeeProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "EmployeeProject",
                type: "TEXT",
                nullable: true,
                defaultValueSql: "'employee'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "EmployeeProject");
        }
    }
}
