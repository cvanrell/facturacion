using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WIS.Billing.DataAccessCore.Migrations
{
    public partial class Migration10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupportRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    IVA = table.Column<int>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    Periodicity = table.Column<string>(nullable: true),
                    AdjustmentPeriodicity = table.Column<string>(nullable: true),
                    SpecialDiscount = table.Column<decimal>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportRates_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupportRates_ClientId",
                table: "SupportRates",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportRates");
        }
    }
}
