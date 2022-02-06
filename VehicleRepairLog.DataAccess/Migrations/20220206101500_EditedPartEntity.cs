using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRepairLog.DataAccess.Migrations
{
    public partial class EditedPartEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Repairs_RepairId",
                table: "Parts");

            migrationBuilder.AlterColumn<int>(
                name: "RepairId",
                table: "Parts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Repairs_RepairId",
                table: "Parts",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Repairs_RepairId",
                table: "Parts");

            migrationBuilder.AlterColumn<int>(
                name: "RepairId",
                table: "Parts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Repairs_RepairId",
                table: "Parts",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
