using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishForoshi.Database.Migrations
{
    public partial class Barber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarberStaff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarberStaff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerQueues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarberStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerQueueTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerQueues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerQueues_BarberStaff_BarberStaffId",
                        column: x => x.BarberStaffId,
                        principalTable: "BarberStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQueues_BarberStaffId",
                table: "CustomerQueues",
                column: "BarberStaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerQueues");

            migrationBuilder.DropTable(
                name: "BarberStaff");
        }
    }
}
