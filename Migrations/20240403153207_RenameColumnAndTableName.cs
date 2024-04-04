using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myFirstWeb.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnAndTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
             name: "ProjectName",
             table: "Task",
             newName: "Task",
             schema: "dbo");

            migrationBuilder.RenameTable(
            name: "Task",
            schema: "dbo",
            newName: "Tasks",
            newSchema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}
