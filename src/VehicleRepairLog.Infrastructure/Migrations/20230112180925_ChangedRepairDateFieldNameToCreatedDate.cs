using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRepairLog.Infrastructure.Migrations
{
    public partial class ChangedRepairDateFieldNameToCreatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Repairs",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Repairs",
                newName: "Date");
        }
    }
}
