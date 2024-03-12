using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class AlterPriceAlterOrderRoomSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Order__SlotId__70DDC3D8",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Slot");

            migrationBuilder.DropIndex(
                name: "IX_Order_SlotId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "SlotId",
                table: "Order");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ServiceElement",
                type: "decimal(20,1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "PartyPlan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventEnd",
                table: "Order",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventStart",
                table: "Order",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Dish",
                type: "decimal(20,1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ServiceElement");

            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "PartyPlan");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "EventEnd",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "EventStart",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Dish");

            migrationBuilder.AddColumn<Guid>(
                name: "SlotId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Slot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromHour = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "1"),
                    ToHour = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Slot__3214EC0768BFFF5B", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Slot__RoomId__6C190EBB",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_SlotId",
                table: "Order",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_RoomId",
                table: "Slot",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK__Order__SlotId__70DDC3D8",
                table: "Order",
                column: "SlotId",
                principalTable: "Slot",
                principalColumn: "Id");
        }
    }
}
