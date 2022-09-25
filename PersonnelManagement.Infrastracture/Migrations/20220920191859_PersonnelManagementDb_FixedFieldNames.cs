using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class PersonnelManagementDbFixedFieldNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PositionTile",
                table: "Positions",
                newName: "PositionTitle");

            migrationBuilder.RenameColumn(
                name: "DepartmentTile",
                table: "Departments",
                newName: "DepartmentTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PositionTitle",
                table: "Positions",
                newName: "PositionTile");

            migrationBuilder.RenameColumn(
                name: "DepartmentTitle",
                table: "Departments",
                newName: "DepartmentTile");
        }
    }
}
