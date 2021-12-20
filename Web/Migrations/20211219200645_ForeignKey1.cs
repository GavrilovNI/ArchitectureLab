using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    public partial class ForeignKey1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoughtProduct_BoughtCarts_BoughtCartId",
                table: "BoughtProduct");

            migrationBuilder.AlterColumn<long>(
                name: "BoughtCartId",
                table: "BoughtProduct",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "BoughtCarts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_BoughtProduct_BoughtCarts_BoughtCartId",
                table: "BoughtProduct",
                column: "BoughtCartId",
                principalTable: "BoughtCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoughtProduct_BoughtCarts_BoughtCartId",
                table: "BoughtProduct");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "BoughtCarts");

            migrationBuilder.AlterColumn<long>(
                name: "BoughtCartId",
                table: "BoughtProduct",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_BoughtProduct_BoughtCarts_BoughtCartId",
                table: "BoughtProduct",
                column: "BoughtCartId",
                principalTable: "BoughtCarts",
                principalColumn: "Id");
        }
    }
}
