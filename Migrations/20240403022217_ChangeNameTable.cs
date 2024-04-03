using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

#nullable disable

namespace myFirstWeb.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropColumn(
			   name: "Status",
			   table: "Project");

			migrationBuilder.AddColumn<bool>(
			   name: "Complete",
			   table: "Project",
			   type: "bit",
			   nullable: false);

			migrationBuilder.RenameTable(
			name: "Project",
			schema: "dbo",
			newName: "Task",
			newSchema: "dbo");

			
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
