using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class AddRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3116dce2-ab07-4c6f-9b7b-419251d29fc9"), null, "HOST_STAFF", "Nhân viên Quản lý tiệc" },
                    { new Guid("496e92c8-626e-4e49-94b5-59b42e2e61de"), null, "IMPLEMENT_STAFF", "Nhân viên Thực hiện" },
                    { new Guid("78975a1b-ea7f-4c1d-ab67-c5abebc26615"), null, "ADMIN", "Quản lý" },
                    { new Guid("d4514b78-ab78-4346-ae54-b3216702399a"), null, "USER", "Khách hàng" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3116dce2-ab07-4c6f-9b7b-419251d29fc9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("496e92c8-626e-4e49-94b5-59b42e2e61de"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("78975a1b-ea7f-4c1d-ab67-c5abebc26615"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4514b78-ab78-4346-ae54-b3216702399a"));
        }
    }
}
