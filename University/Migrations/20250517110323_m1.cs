using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "disciplini",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "text", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disciplini", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dolznosti",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dolznosti", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stepeni",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stepeni", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "kafedri",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "text", nullable: false),
                    zav = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kafedri", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "prepodavateli",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lastname = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    stepen = table.Column<int>(type: "int", nullable: true),
                    dolznost = table.Column<int>(type: "int", nullable: true),
                    nagruzka = table.Column<int>(type: "int", nullable: false),
                    kafedra = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prepodavateli", x => x.id);
                    table.ForeignKey(
                        name: "FK__prepodava__dolzn__403A8C7D",
                        column: x => x.dolznost,
                        principalTable: "dolznosti",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__prepodava__kafed__412EB0B6",
                        column: x => x.kafedra,
                        principalTable: "kafedri",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__prepodava__stepe__3F466844",
                        column: x => x.stepen,
                        principalTable: "stepeni",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "prepDisciplini",
                columns: table => new
                {
                    prep_id = table.Column<int>(type: "int", nullable: true),
                    disc_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__prepDisci__disc___44FF419A",
                        column: x => x.disc_id,
                        principalTable: "disciplini",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__prepDisci__prep___440B1D61",
                        column: x => x.prep_id,
                        principalTable: "prepodavateli",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_kafedri_zav",
                table: "kafedri",
                column: "zav");

            migrationBuilder.CreateIndex(
                name: "IX_prepDisciplini_disc_id",
                table: "prepDisciplini",
                column: "disc_id");

            migrationBuilder.CreateIndex(
                name: "IX_prepDisciplini_prep_id",
                table: "prepDisciplini",
                column: "prep_id");

            migrationBuilder.CreateIndex(
                name: "IX_prepodavateli_dolznost",
                table: "prepodavateli",
                column: "dolznost");

            migrationBuilder.CreateIndex(
                name: "IX_prepodavateli_kafedra",
                table: "prepodavateli",
                column: "kafedra");

            migrationBuilder.CreateIndex(
                name: "IX_prepodavateli_stepen",
                table: "prepodavateli",
                column: "stepen");

            migrationBuilder.AddForeignKey(
                name: "kafedriToPrepodavateli",
                table: "kafedri",
                column: "zav",
                principalTable: "prepodavateli",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "kafedriToPrepodavateli",
                table: "kafedri");

            migrationBuilder.DropTable(
                name: "prepDisciplini");

            migrationBuilder.DropTable(
                name: "disciplini");

            migrationBuilder.DropTable(
                name: "prepodavateli");

            migrationBuilder.DropTable(
                name: "dolznosti");

            migrationBuilder.DropTable(
                name: "kafedri");

            migrationBuilder.DropTable(
                name: "stepeni");
        }
    }
}
