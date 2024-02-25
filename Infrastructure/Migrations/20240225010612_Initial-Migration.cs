using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermissionsEmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Permissions_PermissionsEmployeeId",
                        column: x => x.PermissionsEmployeeId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PermissionsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionsTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAreas", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkAreaId = table.Column<int>(type: "int", nullable: false),
                    PermissionEmployeesId = table.Column<int>(type: "int", nullable: true),
                    PermissionTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Permissions_PermissionEmployeesId",
                        column: x => x.PermissionEmployeesId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_PermissionsTypes_PermissionTypeId",
                        column: x => x.PermissionTypeId,
                        principalTable: "PermissionsTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_WorkAreas_WorkAreaId",
                        column: x => x.WorkAreaId,
                        principalTable: "WorkAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PermissionEmployeesId",
                table: "Employees",
                column: "PermissionEmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PermissionTypeId",
                table: "Employees",
                column: "PermissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WorkAreaId",
                table: "Employees",
                column: "WorkAreaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionsEmployeeId",
                table: "Permissions",
                column: "PermissionsEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionTypePermissionsEmployee_PermisssionsEmployeesId",
                table: "PermissionTypePermissionsEmployee",
                column: "PermisssionsEmployeesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "PermissionTypePermissionsEmployee");

            migrationBuilder.DropTable(
                name: "WorkAreas");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PermissionsTypes");
        }
    }
}
