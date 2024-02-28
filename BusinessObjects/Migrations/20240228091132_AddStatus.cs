using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class AddStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Voucher",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Slot",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "ServiceElementDetail",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "ServiceElement",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Service",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "RoomType",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Room",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PartyPlan",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "OrderDetail",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Menu",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Feedback",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "ElementType",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "DishType",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Dish",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Deposit",
                type: "bit",
                nullable: false,
                defaultValueSql: "1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Slot");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ServiceElementDetail");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ServiceElement");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RoomType");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PartyPlan");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ElementType");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DishType");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Dish");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Deposit");
        }
    }
}
