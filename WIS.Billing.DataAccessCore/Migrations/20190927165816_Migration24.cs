using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WIS.Billing.DataAccessCore.Migrations
{
    public partial class Migration24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BillNumber = table.Column<long>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    BillDate = table.Column<DateTime>(nullable: false),
                    DT_ADDROW = table.Column<DateTime>(nullable: false),
                    DT_UPDROW = table.Column<DateTime>(nullable: false),
                    FL_DELETED = table.Column<string>(maxLength: 1, nullable: true),
                    SupportId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Supports_SupportId",
                        column: x => x.SupportId,
                        principalTable: "Supports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_GRID_FILTER",
                columns: table => new
                {
                    CD_FILTRO = table.Column<long>(nullable: false),
                    NM_FILTRO = table.Column<string>(maxLength: 20, nullable: false),
                    DS_FILTRO = table.Column<string>(maxLength: 200, nullable: true),
                    CD_BLOQUE = table.Column<string>(maxLength: 40, nullable: false),
                    CD_APLICACION = table.Column<string>(maxLength: 40, nullable: false),
                    DT_ADDROW = table.Column<DateTime>(nullable: true),
                    USERID = table.Column<int>(nullable: false),
                    FL_GLOBAL = table.Column<string>(maxLength: 1, nullable: true),
                    FL_INICIAL = table.Column<string>(maxLength: 1, nullable: true),
                    VL_FILTRO_AVANZADO = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRID_FILTER", x => x.CD_FILTRO);
                });

            migrationBuilder.CreateTable(
                name: "T_LOG_BILL",
                columns: table => new
                {
                    NU_LOG = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USER = table.Column<int>(nullable: false),
                    DT_ADDROW = table.Column<DateTime>(nullable: false),
                    ACTION = table.Column<string>(nullable: true),
                    DATA = table.Column<string>(type: "nvarchar(4000)", nullable: true),
                    PAGE = table.Column<string>(nullable: true),
                    ID_BILL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LOG_BILL", x => x.NU_LOG);
                });

            migrationBuilder.CreateTable(
                name: "T_GRID_FILTER_DET",
                columns: table => new
                {
                    CD_FILTRO = table.Column<long>(nullable: false),
                    CD_COLUMNA = table.Column<string>(maxLength: 40, nullable: false),
                    VL_FILTRO = table.Column<string>(maxLength: 2000, nullable: true),
                    VL_ORDEN = table.Column<short>(nullable: true),
                    NU_ORDEN_EJECUCION = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRID_FILTER_DET", x => new { x.CD_FILTRO, x.CD_COLUMNA });
                    table.UniqueConstraint("AK_T_GRID_FILTER_DET_CD_COLUMNA_CD_FILTRO", x => new { x.CD_COLUMNA, x.CD_FILTRO });
                    table.ForeignKey(
                        name: "FK_T_GRID_FILTER_DET_T_GRID_FILTER_CD_FILTRO",
                        column: x => x.CD_FILTRO,
                        principalTable: "T_GRID_FILTER",
                        principalColumn: "CD_FILTRO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_SupportId",
                table: "Bills",
                column: "SupportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "T_GRID_FILTER_DET");

            migrationBuilder.DropTable(
                name: "T_LOG_BILL");

            migrationBuilder.DropTable(
                name: "T_GRID_FILTER");
        }
    }
}
