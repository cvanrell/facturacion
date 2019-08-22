using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WIS.Billing.DataAccessCore.Migrations
{
    public partial class Migration20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DT_ADDROW",
                table: "Adjustments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_UPDROW",
                table: "Adjustments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "T_LOG_ADJUSTMENT",
                columns: table => new
                {
                    NU_LOG = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    USER = table.Column<int>(nullable: false),
                    DT_ADDROW = table.Column<DateTime>(nullable: false),
                    ACTION = table.Column<string>(nullable: true),
                    DATA = table.Column<string>(type: "nvarchar(4000)", nullable: true),
                    PAGE = table.Column<string>(nullable: true),
                    ID_ADJUSTMENT = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LOG_ADJUSTMENT", x => x.NU_LOG);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_LOG_ADJUSTMENT");

            migrationBuilder.DropColumn(
                name: "DT_ADDROW",
                table: "Adjustments");

            migrationBuilder.DropColumn(
                name: "DT_UPDROW",
                table: "Adjustments");
        }
    }
}
