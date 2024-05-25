using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myFirstWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnStatus2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Project",
                type: "bit",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Employee_ID_employeeID",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ID_employeeID",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_employeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Projects_Employee_ID_employeeID",
                        column: x => x.ID_employeeID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ID_employeeID",
                table: "Projects",
                column: "ID_employeeID");
        }
    }
}
