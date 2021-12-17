using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    public partial class BoughtCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BoughtProducts",
                table: "BoughtProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BoughtProducts");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "BoughtProducts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BoughtProducts");

            migrationBuilder.RenameTable(
                name: "BoughtProducts",
                newName: "BoughtProduct");

            migrationBuilder.RenameColumn(
                name: "Paid",
                table: "BoughtProduct",
                newName: "PaidStatus");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "BoughtProduct",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<long>(
                name: "BoughtCartId",
                table: "BoughtProduct",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoughtProduct",
                table: "BoughtProduct",
                column: "ProductId");

            migrationBuilder.CreateTable(
                name: "BoughtCarts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoughtCarts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoughtProduct_BoughtCartId",
                table: "BoughtProduct",
                column: "BoughtCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoughtProduct_BoughtCarts_BoughtCartId",
                table: "BoughtProduct",
                column: "BoughtCartId",
                principalTable: "BoughtCarts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoughtProduct_BoughtCarts_BoughtCartId",
                table: "BoughtProduct");

            migrationBuilder.DropTable(
                name: "BoughtCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoughtProduct",
                table: "BoughtProduct");

            migrationBuilder.DropIndex(
                name: "IX_BoughtProduct_BoughtCartId",
                table: "BoughtProduct");

            migrationBuilder.DropColumn(
                name: "BoughtCartId",
                table: "BoughtProduct");

            migrationBuilder.RenameTable(
                name: "BoughtProduct",
                newName: "BoughtProducts");

            migrationBuilder.RenameColumn(
                name: "PaidStatus",
                table: "BoughtProducts",
                newName: "Paid");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "BoughtProducts",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "BoughtProducts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "BoughtProducts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BoughtProducts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoughtProducts",
                table: "BoughtProducts",
                column: "Id");
        }
    }
}
