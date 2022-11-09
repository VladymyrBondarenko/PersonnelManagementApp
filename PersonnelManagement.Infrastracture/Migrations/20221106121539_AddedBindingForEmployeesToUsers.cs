using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddedBindingForEmployeesToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Positions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 6, 12, 15, 39, 619, DateTimeKind.Utc).AddTicks(5819),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 6, 11, 52, 23, 582, DateTimeKind.Utc).AddTicks(9559));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Originals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 6, 12, 15, 39, 619, DateTimeKind.Utc).AddTicks(6928),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 6, 11, 52, 23, 583, DateTimeKind.Utc).AddTicks(511));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 6, 12, 15, 39, 619, DateTimeKind.Utc).AddTicks(6186),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 6, 11, 52, 23, 582, DateTimeKind.Utc).AddTicks(9854));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 6, 12, 15, 39, 619, DateTimeKind.Utc).AddTicks(6557),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 6, 11, 52, 23, 583, DateTimeKind.Utc).AddTicks(202));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 6, 12, 15, 39, 619, DateTimeKind.Utc).AddTicks(5279),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 6, 11, 52, 23, 582, DateTimeKind.Utc).AddTicks(9081));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Employees_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Employees_EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Positions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 6, 11, 52, 23, 582, DateTimeKind.Utc).AddTicks(9559),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 6, 12, 15, 39, 619, DateTimeKind.Utc).AddTicks(5819));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Originals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 6, 11, 52, 23, 583, DateTimeKind.Utc).AddTicks(511),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 6, 12, 15, 39, 619, DateTimeKind.Utc).AddTicks(6928));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 6, 11, 52, 23, 582, DateTimeKind.Utc).AddTicks(9854),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 6, 12, 15, 39, 619, DateTimeKind.Utc).AddTicks(6186));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 6, 11, 52, 23, 583, DateTimeKind.Utc).AddTicks(202),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 6, 12, 15, 39, 619, DateTimeKind.Utc).AddTicks(6557));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 6, 11, 52, 23, 582, DateTimeKind.Utc).AddTicks(9081),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 6, 12, 15, 39, 619, DateTimeKind.Utc).AddTicks(5279));
        }
    }
}
