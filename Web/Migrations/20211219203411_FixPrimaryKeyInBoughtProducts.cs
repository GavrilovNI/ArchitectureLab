using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    public partial class FixPrimaryKeyInBoughtProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BoughtProduct",
                table: "BoughtProduct");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "BoughtProduct",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoughtProduct",
                table: "BoughtProduct",
                columns: new[] { "ProductId", "BoughtCartId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BoughtProduct",
                table: "BoughtProduct");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "BoughtProduct",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoughtProduct",
                table: "BoughtProduct",
                column: "ProductId");
        }
    }
}
