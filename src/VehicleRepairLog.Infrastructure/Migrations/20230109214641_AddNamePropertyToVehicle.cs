using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRepairLog.Infrastructure.Migrations
{
    public partial class AddNamePropertyToVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Vehicles",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Vehicles");
        }
    }
}
