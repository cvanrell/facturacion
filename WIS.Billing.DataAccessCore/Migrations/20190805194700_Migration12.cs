using Microsoft.EntityFrameworkCore.Migrations;

namespace WIS.Billing.DataAccessCore.Migrations
{
    public partial class Migration12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Periodicity",
                table: "Maintenances");

            migrationBuilder.AddColumn<string>(
                name: "FL_DELETED",
                table: "SupportRates",
                maxLength: 1,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FL_DELETED",
                table: "SupportRates");

            migrationBuilder.AddColumn<int>(
                name: "Periodicity",
                table: "Maintenances",
                nullable: false,
                defaultValue: 0);
        }
    }
}
