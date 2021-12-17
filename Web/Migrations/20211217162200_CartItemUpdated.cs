using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    public partial class CartItemUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Carts_CartUserId",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_CartUserId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "CartUserId",
                table: "CartItem");

            migrationBuilder.RenameTable(
                name: "CartItem",
                newName: "CartItems");

            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "CartItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "CartItems");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "CartItem");

            migrationBuilder.AddColumn<string>(
                name: "CartUserId",
                table: "CartItem",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartUserId",
                table: "CartItem",
                column: "CartUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Carts_CartUserId",
                table: "CartItem",
                column: "CartUserId",
                principalTable: "Carts",
                principalColumn: "UserId");
        }
    }
}
