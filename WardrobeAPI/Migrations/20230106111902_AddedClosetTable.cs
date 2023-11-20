using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WardrobeAPI.Migrations
{
    public partial class AddedClosetTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Closet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Closet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apparel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(16)", nullable: false),
                    ClosetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apparel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apparel_Closet_ClosetId",
                        column: x => x.ClosetId,
                        principalTable: "Closet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Closet",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "det grå skab" });

            migrationBuilder.InsertData(
                table: "Closet",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Det grønne skab" });

            migrationBuilder.InsertData(
                table: "Apparel",
                columns: new[] { "Id", "ClosetId", "Color", "Description", "Title" },
                values: new object[] { 1, 1, "Blå", "Den blå skjorte med hvide prikker", "Den blå Skjorte" });

            migrationBuilder.InsertData(
                table: "Apparel",
                columns: new[] { "Id", "ClosetId", "Color", "Description", "Title" },
                values: new object[] { 2, 2, "Sort", "Det storte wrangler", "Sorte Jeans" });

            migrationBuilder.CreateIndex(
                name: "IX_Apparel_ClosetId",
                table: "Apparel",
                column: "ClosetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apparel");

            migrationBuilder.DropTable(
                name: "Closet");
        }
    }
}
