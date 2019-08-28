using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WIS.Billing.DataAccessCore.Migrations
{
    public partial class Migration21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fee_Projects_ProjectId",
                table: "Fee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fee",
                table: "Fee");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Fee");

            migrationBuilder.RenameTable(
                name: "Fee",
                newName: "Fees");

            migrationBuilder.RenameColumn(
                name: "USER",
                table: "T_LOG_SUPPORT_RATE",
                newName: "ID_USER");

            migrationBuilder.RenameColumn(
                name: "USER",
                table: "T_LOG_PROJECT",
                newName: "ID_USER");

            migrationBuilder.RenameColumn(
                name: "USER",
                table: "T_LOG_HOUR_RATE",
                newName: "ID_USER");

            migrationBuilder.RenameColumn(
                name: "USER",
                table: "T_LOG_CLIENT",
                newName: "ID_USER");

            migrationBuilder.RenameColumn(
                name: "USER",
                table: "T_LOG_ADJUSTMENT",
                newName: "ID_USER");

            migrationBuilder.RenameColumn(
                name: "FL_FOREIGN",
                table: "Clients",
                newName: "FL_IVA");

            migrationBuilder.RenameIndex(
                name: "IX_Fee_ProjectId",
                table: "Fees",
                newName: "IX_Fees_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fees",
                table: "Fees",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "T_LOG_FEE",
                columns: table => new
                {
                    NU_LOG = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USER = table.Column<int>(nullable: false),
                    DT_ADDROW = table.Column<DateTime>(nullable: false),
                    ACTION = table.Column<string>(nullable: true),
                    DATA = table.Column<string>(type: "nvarchar(4000)", nullable: true),
                    PAGE = table.Column<string>(nullable: true),
                    ID_FEE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LOG_FEE", x => x.NU_LOG);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Fees_Projects_ProjectId",
                table: "Fees",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fees_Projects_ProjectId",
                table: "Fees");

            migrationBuilder.DropTable(
                name: "T_LOG_FEE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fees",
                table: "Fees");

            migrationBuilder.RenameTable(
                name: "Fees",
                newName: "Fee");

            migrationBuilder.RenameColumn(
                name: "ID_USER",
                table: "T_LOG_SUPPORT_RATE",
                newName: "USER");

            migrationBuilder.RenameColumn(
                name: "ID_USER",
                table: "T_LOG_PROJECT",
                newName: "USER");

            migrationBuilder.RenameColumn(
                name: "ID_USER",
                table: "T_LOG_HOUR_RATE",
                newName: "USER");

            migrationBuilder.RenameColumn(
                name: "ID_USER",
                table: "T_LOG_CLIENT",
                newName: "USER");

            migrationBuilder.RenameColumn(
                name: "ID_USER",
                table: "T_LOG_ADJUSTMENT",
                newName: "USER");

            migrationBuilder.RenameColumn(
                name: "FL_IVA",
                table: "Clients",
                newName: "FL_FOREIGN");

            migrationBuilder.RenameIndex(
                name: "IX_Fees_ProjectId",
                table: "Fee",
                newName: "IX_Fee_ProjectId");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Fee",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fee",
                table: "Fee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fee_Projects_ProjectId",
                table: "Fee",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
