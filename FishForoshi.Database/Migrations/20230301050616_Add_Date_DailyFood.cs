using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishForoshi.Database.Migrations
{
    public partial class Add_Date_DailyFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "DailyFoods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "DailyFoods");
        }
    }
}
