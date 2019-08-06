using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WIS.Billing.DataAccessCore.Migrations
{
    public partial class Migration13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Installments",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IVA",
                table: "Fee");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Fee",
                newName: "Discount");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "InitialDate",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Projects",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "MonthYear",
                table: "Fee",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "MonthYear",
                table: "Fee");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Fee",
                newName: "Total");

            migrationBuilder.AlterColumn<int>(
                name: "Currency",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Installments",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "IVA",
                table: "Fee",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
