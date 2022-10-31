using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Part_B.Database.Migrations.Current
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Glossary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Term = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Definition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glossary", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Glossary_Term",
                table: "Glossary",
                column: "Term");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Glossary");
        }
    }
}
