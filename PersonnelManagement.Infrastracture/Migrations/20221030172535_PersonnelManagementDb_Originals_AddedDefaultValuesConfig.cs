using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class PersonnelManagementDbOriginalsAddedDefaultValuesConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Positions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 30, 17, 25, 35, 330, DateTimeKind.Utc).AddTicks(2351),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 30, 17, 15, 10, 569, DateTimeKind.Utc).AddTicks(9302));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Originals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 30, 17, 25, 35, 330, DateTimeKind.Utc).AddTicks(3366),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 30, 17, 25, 35, 330, DateTimeKind.Utc).AddTicks(2610),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 30, 17, 25, 35, 330, DateTimeKind.Utc).AddTicks(3043),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 30, 17, 25, 35, 330, DateTimeKind.Utc).AddTicks(1995),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 30, 17, 15, 10, 569, DateTimeKind.Utc).AddTicks(8963));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Positions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 30, 17, 15, 10, 569, DateTimeKind.Utc).AddTicks(9302),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 30, 17, 25, 35, 330, DateTimeKind.Utc).AddTicks(2351));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Originals",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 30, 17, 25, 35, 330, DateTimeKind.Utc).AddTicks(3366));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 30, 17, 25, 35, 330, DateTimeKind.Utc).AddTicks(2610));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 30, 17, 25, 35, 330, DateTimeKind.Utc).AddTicks(3043));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 30, 17, 15, 10, 569, DateTimeKind.Utc).AddTicks(8963),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 30, 17, 25, 35, 330, DateTimeKind.Utc).AddTicks(1995));
        }
    }
}
