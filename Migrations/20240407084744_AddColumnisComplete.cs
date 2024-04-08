using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myFirstWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnisComplete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isComplete",
                table: "ProjectEmployees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isComplete",
                table: "ProjectEmployees");
        }
    }
}
