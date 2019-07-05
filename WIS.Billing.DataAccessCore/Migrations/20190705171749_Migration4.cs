using Microsoft.EntityFrameworkCore.Migrations;

namespace WIS.Billing.DataAccessCore.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_GRID_DEFAULT_CONFIG",
                columns: table => new
                {
                    CD_APLICACION = table.Column<string>(maxLength: 30, nullable: false),
                    CD_BLOQUE = table.Column<string>(maxLength: 30, nullable: false),
                    NM_DATAFIELD = table.Column<string>(maxLength: 30, nullable: false),
                    DS_COLUMNA = table.Column<string>(maxLength: 100, nullable: true),
                    NU_ORDEN_VISUAL = table.Column<short>(nullable: true),
                    FL_VISIBLE = table.Column<string>(maxLength: 1, nullable: false),
                    RESOURCEID = table.Column<int>(nullable: true),
                    VL_ALINEACION = table.Column<string>(maxLength: 1, nullable: false),
                    DS_DATA_FORMAT_STRING = table.Column<string>(maxLength: 30, nullable: true),
                    VL_WIDTH = table.Column<decimal>(nullable: true),
                    VL_TYPE = table.Column<string>(maxLength: 2, nullable: false),
                    VL_LINK = table.Column<string>(maxLength: 500, nullable: true),
                    VL_POSICION_FIJADO = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRID_DEFAULT_CONFIG", x => new { x.CD_APLICACION, x.CD_BLOQUE, x.NM_DATAFIELD });
                });

            migrationBuilder.CreateTable(
                name: "T_GRID_USER_CONFIG",
                columns: table => new
                {
                    CD_APLICACION = table.Column<string>(maxLength: 30, nullable: false),
                    CD_BLOQUE = table.Column<string>(maxLength: 30, nullable: false),
                    NM_DATAFIELD = table.Column<string>(maxLength: 30, nullable: false),
                    DS_COLUMNA = table.Column<string>(maxLength: 100, nullable: true),
                    NU_ORDEN_VISUAL = table.Column<short>(nullable: true),
                    FL_VISIBLE = table.Column<string>(maxLength: 1, nullable: false),
                    RESOURCEID = table.Column<int>(nullable: true),
                    VL_ALINEACION = table.Column<string>(maxLength: 1, nullable: false),
                    DS_DATA_FORMAT_STRING = table.Column<string>(maxLength: 30, nullable: true),
                    VL_WIDTH = table.Column<decimal>(nullable: true),
                    VL_TYPE = table.Column<string>(maxLength: 2, nullable: false),
                    VL_LINK = table.Column<string>(maxLength: 500, nullable: true),
                    VL_POSICION_FIJADO = table.Column<short>(nullable: true),
                    USERID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRID_USER_CONFIG", x => new { x.CD_APLICACION, x.CD_BLOQUE, x.NM_DATAFIELD });
                    table.UniqueConstraint("AK_T_GRID_USER_CONFIG_USERID", x => x.USERID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_GRID_DEFAULT_CONFIG");

            migrationBuilder.DropTable(
                name: "T_GRID_USER_CONFIG");
        }
    }
}
