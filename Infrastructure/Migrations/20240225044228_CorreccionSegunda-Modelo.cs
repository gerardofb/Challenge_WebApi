using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class CorreccionSegundaModelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_WorkAreas_WorkAreaId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_WorkAreaId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "WorkAreaId",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "EmployeeWorkArea",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    WorkAreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeWorkArea", x => new { x.EmployeeId, x.WorkAreaId });
                    table.ForeignKey(
                        name: "FK_EmployeeWorkArea_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeWorkArea_WorkAreas_WorkAreaId",
                        column: x => x.WorkAreaId,
                        principalTable: "WorkAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorkArea_WorkAreaId",
                table: "EmployeeWorkArea",
                column: "WorkAreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeWorkArea");

            migrationBuilder.AddColumn<int>(
                name: "WorkAreaId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WorkAreaId",
                table: "Employees",
                column: "WorkAreaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_WorkAreas_WorkAreaId",
                table: "Employees",
                column: "WorkAreaId",
                principalTable: "WorkAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
