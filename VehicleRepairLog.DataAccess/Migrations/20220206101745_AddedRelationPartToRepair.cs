using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRepairLog.DataAccess.Migrations
{
    public partial class AddedRelationPartToRepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Repairs_RepairId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_RepairId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "RepairId",
                table: "Parts");

            migrationBuilder.CreateTable(
                name: "PartRepair",
                columns: table => new
                {
                    PartsId = table.Column<int>(type: "int", nullable: false),
                    RepairsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartRepair", x => new { x.PartsId, x.RepairsId });
                    table.ForeignKey(
                        name: "FK_PartRepair_Parts_PartsId",
                        column: x => x.PartsId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartRepair_Repairs_RepairsId",
                        column: x => x.RepairsId,
                        principalTable: "Repairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartRepair_RepairsId",
                table: "PartRepair",
                column: "RepairsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartRepair");

            migrationBuilder.AddColumn<int>(
                name: "RepairId",
                table: "Parts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_RepairId",
                table: "Parts",
                column: "RepairId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Repairs_RepairId",
                table: "Parts",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id");
        }
    }
}
