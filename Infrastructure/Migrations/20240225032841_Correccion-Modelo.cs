using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class CorreccionModelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Permissions_PermissionEmployeesId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_PermissionsTypes_PermissionTypeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Permissions_PermissionsEmployeeId",
                table: "Permissions");

            migrationBuilder.DropTable(
                name: "PermissionTypePermissionsEmployee");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_PermissionsEmployeeId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PermissionEmployeesId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PermissionTypeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PermissionsEmployeeId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "PermissionEmployeesId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PermissionTypeId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "PermissionTypeId",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EmployeePermissionsEmployee",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    PermissionEmployeesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePermissionsEmployee", x => new { x.EmployeesId, x.PermissionEmployeesId });
                    table.ForeignKey(
                        name: "FK_EmployeePermissionsEmployee_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePermissionsEmployee_Permissions_PermissionEmployeesId",
                        column: x => x.PermissionEmployeesId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionTypeId",
                table: "Permissions",
                column: "PermissionTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePermissionsEmployee_PermissionEmployeesId",
                table: "EmployeePermissionsEmployee",
                column: "PermissionEmployeesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_PermissionsTypes_PermissionTypeId",
                table: "Permissions",
                column: "PermissionTypeId",
                principalTable: "PermissionsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_PermissionsTypes_PermissionTypeId",
                table: "Permissions");

            migrationBuilder.DropTable(
                name: "EmployeePermissionsEmployee");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_PermissionTypeId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "PermissionTypeId",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionsEmployeeId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermissionEmployeesId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermissionTypeId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PermissionTypePermissionsEmployee",
                columns: table => new
                {
                    PermissionTypesId = table.Column<int>(type: "int", nullable: false),
                    PermisssionsEmployeesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTypePermissionsEmployee", x => new { x.PermissionTypesId, x.PermisssionsEmployeesId });
                    table.ForeignKey(
                        name: "FK_PermissionTypePermissionsEmployee_Permissions_PermisssionsEmployeesId",
                        column: x => x.PermisssionsEmployeesId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionTypePermissionsEmployee_PermissionsTypes_PermissionTypesId",
                        column: x => x.PermissionTypesId,
                        principalTable: "PermissionsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionsEmployeeId",
                table: "Permissions",
                column: "PermissionsEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PermissionEmployeesId",
                table: "Employees",
                column: "PermissionEmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PermissionTypeId",
                table: "Employees",
                column: "PermissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionTypePermissionsEmployee_PermisssionsEmployeesId",
                table: "PermissionTypePermissionsEmployee",
                column: "PermisssionsEmployeesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Permissions_PermissionEmployeesId",
                table: "Employees",
                column: "PermissionEmployeesId",
                principalTable: "Permissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_PermissionsTypes_PermissionTypeId",
                table: "Employees",
                column: "PermissionTypeId",
                principalTable: "PermissionsTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Permissions_PermissionsEmployeeId",
                table: "Permissions",
                column: "PermissionsEmployeeId",
                principalTable: "Permissions",
                principalColumn: "Id");
        }
    }
}
