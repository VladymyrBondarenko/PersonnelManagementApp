using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class PersonnelManagementDbAddedOrdersToOrdersDescs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderDescriptionId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderDescriptionId",
                table: "Orders",
                column: "OrderDescriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderDescriptionId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderDescriptionId",
                table: "Orders",
                column: "OrderDescriptionId",
                unique: true);
        }
    }
}
