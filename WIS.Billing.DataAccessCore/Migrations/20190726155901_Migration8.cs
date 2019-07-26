using Microsoft.EntityFrameworkCore.Migrations;

namespace WIS.Billing.DataAccessCore.Migrations
{
    public partial class Migration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "HourRates");

            migrationBuilder.DropColumn(
                name: "StartingMonth",
                table: "HourRates");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "HourRates",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "AdjustmentPeriodicity",
                table: "HourRates",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "FL_DELETED",
                table: "Clients",
                maxLength: 1,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FL_DELETED",
                table: "Clients");

            migrationBuilder.AlterColumn<int>(
                name: "Currency",
                table: "HourRates",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdjustmentPeriodicity",
                table: "HourRates",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "HourRates",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StartingMonth",
                table: "HourRates",
                nullable: false,
                defaultValue: 0);
        }
    }
}
