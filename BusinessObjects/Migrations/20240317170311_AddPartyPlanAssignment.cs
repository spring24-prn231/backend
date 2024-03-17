using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class AddPartyPlanAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartyPlanAssignment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartyPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyPlanAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyPlanAssignment_AspNetUsers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyPlanAssignment_PartyPlan_PartyPlanId",
                        column: x => x.PartyPlanId,
                        principalTable: "PartyPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartyPlanAssignment_PartyPlanId",
                table: "PartyPlanAssignment",
                column: "PartyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyPlanAssignment_StaffId",
                table: "PartyPlanAssignment",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyPlanAssignment");
        }
    }
}
