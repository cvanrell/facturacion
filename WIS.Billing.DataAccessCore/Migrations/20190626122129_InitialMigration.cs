using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WIS.Billing.DataAccessCore.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    RUT = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HourRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: true),
                    Currency = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    AdjustmentPeriodicity = table.Column<int>(nullable: false),
                    StartingMonth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HourRates_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    IVA = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    Periodicity = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: true),
                    Installments = table.Column<int>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    IVA = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Developments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    TicketId = table.Column<string>(nullable: true),
                    TotalHours = table.Column<decimal>(nullable: false),
                    RateId = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Developments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Developments_HourRates_RateId",
                        column: x => x.RateId,
                        principalTable: "HourRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    IVA = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fee_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Developments_ClientId",
                table: "Developments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Developments_RateId",
                table: "Developments",
                column: "RateId");

            migrationBuilder.CreateIndex(
                name: "IX_Fee_ProjectId",
                table: "Fee",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_HourRates_ClientId",
                table: "HourRates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_ClientId",
                table: "Maintenances",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientId",
                table: "Projects",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Developments");

            migrationBuilder.DropTable(
                name: "Fee");

            migrationBuilder.DropTable(
                name: "Maintenances");

            migrationBuilder.DropTable(
                name: "HourRates");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
