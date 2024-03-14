using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class MaxGuest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2b7d9654-914f-494e-bcad-9cb9aa53c078"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6f0a8e56-fdfa-44f2-a507-f563b6c56a7e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8536d142-d931-4532-8b32-c17db646536c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d3a8a043-e475-4f89-9b50-d22585fee0f2"));

            migrationBuilder.AddColumn<decimal>(
                name: "MaxGuest",
                table: "Order",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxGuest",
                table: "Order");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2b7d9654-914f-494e-bcad-9cb9aa53c078"), null, "ADMIN", "ADMIN" },
                    { new Guid("6f0a8e56-fdfa-44f2-a507-f563b6c56a7e"), null, "IMPLEMENT_STAFF", "IMPLEMENT_STAFF" },
                    { new Guid("8536d142-d931-4532-8b32-c17db646536c"), null, "USER", "USER" },
                    { new Guid("d3a8a043-e475-4f89-9b50-d22585fee0f2"), null, "HOST_STAFF", "HOST_STAFF" }
                });
        }
    }
}
