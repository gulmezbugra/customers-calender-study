using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaatApiDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class KayitAtamaEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kayitlar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoslukID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciID = table.Column<int>(type: "int", nullable: false),
                    KullaniciIsmi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Door = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kayitlar", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kayitlar");
        }
    }
}
