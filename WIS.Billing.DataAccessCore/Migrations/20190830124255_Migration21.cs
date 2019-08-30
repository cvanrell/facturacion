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

            migrationBuilder.DropTable(
                name: "Maintenances");

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
                name: "Supports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Total = table.Column<decimal>(nullable: false),
                    DT_ADDROW = table.Column<DateTime>(nullable: false),
                    DT_UPDROW = table.Column<DateTime>(nullable: false),
                    FL_DELETED = table.Column<string>(maxLength: 1, nullable: true),
                    ClientId = table.Column<Guid>(nullable: true),
                    SupportRateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supports_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supports_SupportRates_SupportRateId",
                        column: x => x.SupportRateId,
                        principalTable: "SupportRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "T_LOG_SUPPORT",
                columns: table => new
                {
                    NU_LOG = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USER = table.Column<int>(nullable: false),
                    DT_ADDROW = table.Column<DateTime>(nullable: false),
                    ACTION = table.Column<string>(nullable: true),
                    DATA = table.Column<string>(type: "nvarchar(4000)", nullable: true),
                    PAGE = table.Column<string>(nullable: true),
                    ID_SUPPORT = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LOG_SUPPORT", x => x.NU_LOG);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Supports_ClientId",
                table: "Supports",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Supports_SupportRateId",
                table: "Supports",
                column: "SupportRateId");

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
                name: "Supports");

            migrationBuilder.DropTable(
                name: "T_LOG_FEE");

            migrationBuilder.DropTable(
                name: "T_LOG_SUPPORT");

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

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    Currency = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IVA = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenances_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_ClientId",
                table: "Maintenances",
                column: "ClientId");

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
