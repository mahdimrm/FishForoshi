using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishForoshi.Database.Migrations
{
    public partial class Update_CustomerQueue_DataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerQueueTime",
                table: "CustomerQueues",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CustomerQueueTime",
                table: "CustomerQueues",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
