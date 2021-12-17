using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    public partial class CartUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Carts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    ItemId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    CartUserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_CartItem_Carts_CartUserId",
                        column: x => x.CartUserId,
                        principalTable: "Carts",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartUserId",
                table: "CartItem",
                column: "CartUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.AddColumn<long>(
                name: "ItemId",
                table: "Carts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Carts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                columns: new[] { "UserId", "ItemId" });
        }
    }
}
