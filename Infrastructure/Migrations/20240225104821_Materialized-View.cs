using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class MaterializedView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("EXEC('CREATE VIEW dbo.viewPermissions WITH SCHEMABINDING AS " +
"SELECT EMP.ID, EMP.NAME AS USERNAME, EMP.LASTNAME AS LASTNAME, P.LASTUPDATED, PT.NAME, P.GUID " +
"FROM DBO.EMPLOYEES EMP INNER JOIN DBO.EMPLOYEEPERMISSIONSEMPLOYEE EPERM ON EMP.ID = EPERM.EMPLOYEESID " +
"INNER JOIN DBO.PERMISSIONS P ON P.ID = EPERM.PERMISSIONEMPLOYEESID INNER JOIN DBO.PERMISSIONSTYPES PT ON PT.ID = P.PERMISSIONTYPEID;');");
            migrationBuilder.Sql("EXEC('SET NUMERIC_ROUNDABORT OFF; " +
"SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT," +
"QUOTED_IDENTIFIER, ANSI_NULLS ON;" +
"CREATE UNIQUE CLUSTERED INDEX IDX_VPermissions " +
"ON dbo.viewPermissions (LASTUPDATED, GUID);');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW dbo.viewPermissions;");
        }
    }
}
