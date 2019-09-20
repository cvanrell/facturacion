using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WIS.Billing.DataAccessCore.Migrations
{
    public partial class Migration22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_LOG_ADJUSTMENT");

            migrationBuilder.AddColumn<long>(
                name: "LogAdjustmentNU_LOG",
                table: "T_LOG_SUPPORT_RATE",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LogAdjustmentNU_LOG",
                table: "T_LOG_HOUR_RATE",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "T_LOG_IPC",
                columns: table => new
                {
                    NU_LOG = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USER = table.Column<int>(nullable: false),
                    DT_ADDROW = table.Column<DateTime>(nullable: false),
                    ACTION = table.Column<string>(nullable: true),
                    DATA = table.Column<string>(type: "nvarchar(4000)", nullable: true),
                    PAGE = table.Column<string>(nullable: true),
                    ID_ADJUSTMENT = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LOG_IPC", x => x.NU_LOG);
                });

            migrationBuilder.CreateTable(
                name: "T_LOG_RATE_ADJUSTMENTS",
                columns: table => new
                {
                    NU_LOG = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USER = table.Column<int>(nullable: false),
                    DT_ADDROW = table.Column<DateTime>(nullable: false),
                    ACTION = table.Column<string>(nullable: true),
                    PAGE = table.Column<string>(nullable: true),
                    LogAdjustmentNU_LOG = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LOG_RATE_ADJUSTMENTS", x => x.NU_LOG);
                    table.ForeignKey(
                        name: "FK_T_LOG_RATE_ADJUSTMENTS_T_LOG_IPC_LogAdjustmentNU_LOG",
                        column: x => x.LogAdjustmentNU_LOG,
                        principalTable: "T_LOG_IPC",
                        principalColumn: "NU_LOG",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_LOG_SUPPORT_RATE_LogAdjustmentNU_LOG",
                table: "T_LOG_SUPPORT_RATE",
                column: "LogAdjustmentNU_LOG");

            migrationBuilder.CreateIndex(
                name: "IX_T_LOG_HOUR_RATE_LogAdjustmentNU_LOG",
                table: "T_LOG_HOUR_RATE",
                column: "LogAdjustmentNU_LOG");

            migrationBuilder.CreateIndex(
                name: "IX_T_LOG_RATE_ADJUSTMENTS_LogAdjustmentNU_LOG",
                table: "T_LOG_RATE_ADJUSTMENTS",
                column: "LogAdjustmentNU_LOG");

            migrationBuilder.AddForeignKey(
                name: "FK_T_LOG_HOUR_RATE_T_LOG_RATE_ADJUSTMENTS_LogAdjustmentNU_LOG",
                table: "T_LOG_HOUR_RATE",
                column: "LogAdjustmentNU_LOG",
                principalTable: "T_LOG_RATE_ADJUSTMENTS",
                principalColumn: "NU_LOG",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_T_LOG_SUPPORT_RATE_T_LOG_RATE_ADJUSTMENTS_LogAdjustmentNU_LOG",
                table: "T_LOG_SUPPORT_RATE",
                column: "LogAdjustmentNU_LOG",
                principalTable: "T_LOG_RATE_ADJUSTMENTS",
                principalColumn: "NU_LOG",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_LOG_HOUR_RATE_T_LOG_RATE_ADJUSTMENTS_LogAdjustmentNU_LOG",
                table: "T_LOG_HOUR_RATE");

            migrationBuilder.DropForeignKey(
                name: "FK_T_LOG_SUPPORT_RATE_T_LOG_RATE_ADJUSTMENTS_LogAdjustmentNU_LOG",
                table: "T_LOG_SUPPORT_RATE");

            migrationBuilder.DropTable(
                name: "T_LOG_RATE_ADJUSTMENTS");

            migrationBuilder.DropTable(
                name: "T_LOG_IPC");

            migrationBuilder.DropIndex(
                name: "IX_T_LOG_SUPPORT_RATE_LogAdjustmentNU_LOG",
                table: "T_LOG_SUPPORT_RATE");

            migrationBuilder.DropIndex(
                name: "IX_T_LOG_HOUR_RATE_LogAdjustmentNU_LOG",
                table: "T_LOG_HOUR_RATE");

            migrationBuilder.DropColumn(
                name: "LogAdjustmentNU_LOG",
                table: "T_LOG_SUPPORT_RATE");

            migrationBuilder.DropColumn(
                name: "LogAdjustmentNU_LOG",
                table: "T_LOG_HOUR_RATE");

            migrationBuilder.CreateTable(
                name: "T_LOG_ADJUSTMENT",
                columns: table => new
                {
                    NU_LOG = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ACTION = table.Column<string>(nullable: true),
                    DATA = table.Column<string>(type: "nvarchar(4000)", nullable: true),
                    DT_ADDROW = table.Column<DateTime>(nullable: false),
                    ID_ADJUSTMENT = table.Column<string>(nullable: true),
                    ID_USER = table.Column<int>(nullable: false),
                    PAGE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LOG_ADJUSTMENT", x => x.NU_LOG);
                });
        }
    }
}
