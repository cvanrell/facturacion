using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WIS.Billing.DataAccessCore.Migrations
{
    public partial class Migration23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_LOG_RATE_ADJUSTMENTS_T_LOG_IPC_LogAdjustmentNU_LOG",
                table: "T_LOG_RATE_ADJUSTMENTS");

            migrationBuilder.RenameColumn(
                name: "LogAdjustmentNU_LOG",
                table: "T_LOG_RATE_ADJUSTMENTS",
                newName: "LogIPCNU_LOG");

            migrationBuilder.RenameIndex(
                name: "IX_T_LOG_RATE_ADJUSTMENTS_LogAdjustmentNU_LOG",
                table: "T_LOG_RATE_ADJUSTMENTS",
                newName: "IX_T_LOG_RATE_ADJUSTMENTS_LogIPCNU_LOG");

            migrationBuilder.AddColumn<DateTime>(
                name: "DATEIPC",
                table: "T_LOG_IPC",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_T_LOG_RATE_ADJUSTMENTS_T_LOG_IPC_LogIPCNU_LOG",
                table: "T_LOG_RATE_ADJUSTMENTS",
                column: "LogIPCNU_LOG",
                principalTable: "T_LOG_IPC",
                principalColumn: "NU_LOG",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_LOG_RATE_ADJUSTMENTS_T_LOG_IPC_LogIPCNU_LOG",
                table: "T_LOG_RATE_ADJUSTMENTS");

            migrationBuilder.DropColumn(
                name: "DATEIPC",
                table: "T_LOG_IPC");

            migrationBuilder.RenameColumn(
                name: "LogIPCNU_LOG",
                table: "T_LOG_RATE_ADJUSTMENTS",
                newName: "LogAdjustmentNU_LOG");

            migrationBuilder.RenameIndex(
                name: "IX_T_LOG_RATE_ADJUSTMENTS_LogIPCNU_LOG",
                table: "T_LOG_RATE_ADJUSTMENTS",
                newName: "IX_T_LOG_RATE_ADJUSTMENTS_LogAdjustmentNU_LOG");

            migrationBuilder.AddForeignKey(
                name: "FK_T_LOG_RATE_ADJUSTMENTS_T_LOG_IPC_LogAdjustmentNU_LOG",
                table: "T_LOG_RATE_ADJUSTMENTS",
                column: "LogAdjustmentNU_LOG",
                principalTable: "T_LOG_IPC",
                principalColumn: "NU_LOG",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
