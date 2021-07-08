using Microsoft.EntityFrameworkCore.Migrations;

namespace Curso_Arquitetura_Backend.Migrations
{
    public partial class Baseinicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_USUARIO",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Usuario", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "TB_Curso",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    CodigoUsuario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Curso", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_TB_Curso_TB_Usuario_CodigoUsuario",
                        column: x => x.CodigoUsuario,
                        principalTable: "TB_Usuario",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Curso_CodigoUsuario",
                table: "TB_Curso",
                column: "CodigoUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Curso");

            migrationBuilder.DropTable(
                name: "TB_Usuario");
        }
    }
}
