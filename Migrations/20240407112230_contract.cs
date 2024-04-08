using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myFirstWeb.Migrations
{
    /// <inheritdoc />
    public partial class contract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.CreateTable(
                name: "ProjectContracts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isComplete = table.Column<bool>(type: "bit", nullable: false),
                    Developments = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Employee = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectContracts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProjectContracts_Developments_Developments",
                        column: x => x.Developments,
                        principalTable: "Developments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectContracts_Employee_Employee",
                        column: x => x.Employee,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectContracts_Developments",
                table: "ProjectContracts",
                column: "Developments");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectContracts_Employee",
                table: "ProjectContracts",
                column: "Employee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectContracts");

            migrationBuilder.CreateTable(
                name: "ProjectEmployees",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isComplete = table.Column<bool>(type: "bit", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEmployees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Developments_ID",
                        column: x => x.ID,
                        principalTable: "Developments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Employee_ID",
                        column: x => x.ID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
