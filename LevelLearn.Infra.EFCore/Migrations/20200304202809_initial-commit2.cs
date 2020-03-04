using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LevelLearn.Infra.EFCore.Migrations
{
    public partial class initialcommit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instituicoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    NomePesquisa = table.Column<string>(type: "varchar(250)", nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituicoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    NomePesquisa = table.Column<string>(type: "varchar(250)", nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(190)", nullable: true),
                    CPF = table.Column<string>(type: "varchar(11)", nullable: true),
                    Celular = table.Column<string>(type: "varchar(14)", nullable: true),
                    Genero = table.Column<int>(nullable: false),
                    TipoPessoa = table.Column<int>(nullable: false),
                    ImagemUrl = table.Column<string>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    RA = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    NomePesquisa = table.Column<string>(nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sigla = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    InstituicaoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curso_Instituicoes_InstituicaoId",
                        column: x => x.InstituicaoId,
                        principalTable: "Instituicoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PessoaInstituicao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Perfil = table.Column<int>(nullable: false),
                    PessoaId = table.Column<Guid>(nullable: false),
                    InstituicaoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaInstituicao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaInstituicao_Instituicoes_InstituicaoId",
                        column: x => x.InstituicaoId,
                        principalTable: "Instituicoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PessoaInstituicao_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PessoaCurso",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Perfil = table.Column<int>(nullable: false),
                    PessoaId = table.Column<Guid>(nullable: false),
                    CursoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaCurso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaCurso_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PessoaCurso_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curso_InstituicaoId",
                table: "Curso",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Instituicoes_NomePesquisa",
                table: "Instituicoes",
                column: "NomePesquisa");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaCurso_CursoId",
                table: "PessoaCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaCurso_PessoaId",
                table: "PessoaCurso",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaInstituicao_InstituicaoId",
                table: "PessoaInstituicao",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaInstituicao_PessoaId",
                table: "PessoaInstituicao",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_NomePesquisa",
                table: "Pessoas",
                column: "NomePesquisa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PessoaCurso");

            migrationBuilder.DropTable(
                name: "PessoaInstituicao");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Instituicoes");
        }
    }
}
