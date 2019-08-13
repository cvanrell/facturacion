using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WIS.Billing.DataAccessCore.Migrations
{
    public partial class Logs1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DT_ADDROW",
                table: "SupportRates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_UPDROW",
                table: "SupportRates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_ADDROW",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_UPDROW",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_ADDROW",
                table: "HourRates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_UPDROW",
                table: "HourRates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_ADDROW",
                table: "Fee",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_UPDROW",
                table: "Fee",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FL_DELETED",
                table: "Fee",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_ADDROW",
                table: "Clients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_UPDROW",
                table: "Clients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DT_ADDROW",
                table: "SupportRates");

            migrationBuilder.DropColumn(
                name: "DT_UPDROW",
                table: "SupportRates");

            migrationBuilder.DropColumn(
                name: "DT_ADDROW",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DT_UPDROW",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DT_ADDROW",
                table: "HourRates");

            migrationBuilder.DropColumn(
                name: "DT_UPDROW",
                table: "HourRates");

            migrationBuilder.DropColumn(
                name: "DT_ADDROW",
                table: "Fee");

            migrationBuilder.DropColumn(
                name: "DT_UPDROW",
                table: "Fee");

            migrationBuilder.DropColumn(
                name: "FL_DELETED",
                table: "Fee");

            migrationBuilder.DropColumn(
                name: "DT_ADDROW",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "DT_UPDROW",
                table: "Clients");
        }
    }
}
