using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LevelLearn.Persistence.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Desafios",
                columns: table => new
                {
                    DesafioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Moedas = table.Column<decimal>(nullable: false),
                    Pedra = table.Column<int>(nullable: false),
                    Imagem = table.Column<string>(nullable: false),
                    IsCompletaUmaVez = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desafios", x => x.DesafioId);
                });

            migrationBuilder.CreateTable(
                name: "Instituicoes",
                columns: table => new
                {
                    InstituicaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituicoes", x => x.InstituicaoId);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    PessoaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Sexo = table.Column<int>(nullable: false),
                    TipoPessoa = table.Column<int>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Imagem = table.Column<string>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: true),
                    RA = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.PessoaId);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    TimeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.TimeId);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CursoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    InstituicaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.CursoId);
                    table.ForeignKey(
                        name: "FK_Cursos_Instituicoes_InstituicaoId",
                        column: x => x.InstituicaoId,
                        principalTable: "Instituicoes",
                        principalColumn: "InstituicaoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    NotificacaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Link = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: false),
                    Titulo = table.Column<string>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    IsVisualizada = table.Column<bool>(nullable: false),
                    PessoaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.NotificacaoId);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PessoaInstituicoes",
                columns: table => new
                {
                    PessoaInstituicaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Perfil = table.Column<int>(nullable: false),
                    PessoaId = table.Column<int>(nullable: false),
                    InstituicaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaInstituicoes", x => x.PessoaInstituicaoId);
                    table.ForeignKey(
                        name: "FK_PessoaInstituicoes_Instituicoes_InstituicaoId",
                        column: x => x.InstituicaoId,
                        principalTable: "Instituicoes",
                        principalColumn: "InstituicaoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PessoaInstituicoes_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlunoTimes",
                columns: table => new
                {
                    AlunoTimeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsCriador = table.Column<bool>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    TimeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoTimes", x => x.AlunoTimeId);
                    table.ForeignKey(
                        name: "FK_AlunoTimes_Pessoas_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlunoTimes_Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "TimeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PessoaCursos",
                columns: table => new
                {
                    PessoaCursoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Perfil = table.Column<int>(nullable: false),
                    PessoaId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaCursos", x => x.PessoaCursoId);
                    table.ForeignKey(
                        name: "FK_PessoaCursos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PessoaCursos_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    TurmaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Meta = table.Column<decimal>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    ProfessorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.TurmaId);
                    table.ForeignKey(
                        name: "FK_Turmas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turmas_Pessoas_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlunoDesafios",
                columns: table => new
                {
                    AlunoDesafioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    MoedasGanha = table.Column<decimal>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    DesafioId = table.Column<int>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoDesafios", x => x.AlunoDesafioId);
                    table.ForeignKey(
                        name: "FK_AlunoDesafios_Pessoas_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlunoDesafios_Desafios_DesafioId",
                        column: x => x.DesafioId,
                        principalTable: "Desafios",
                        principalColumn: "DesafioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlunoDesafios_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "TurmaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlunoTurmas",
                columns: table => new
                {
                    AlunoTurmaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlunoId = table.Column<int>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoTurmas", x => x.AlunoTurmaId);
                    table.ForeignKey(
                        name: "FK_AlunoTurmas_Pessoas_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlunoTurmas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "TurmaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chamadas",
                columns: table => new
                {
                    ChamadaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataAula = table.Column<DateTime>(nullable: false),
                    Periodo = table.Column<int>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamadas", x => x.ChamadaId);
                    table.ForeignKey(
                        name: "FK_Chamadas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "TurmaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Missoes",
                columns: table => new
                {
                    MissaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Objetivo = table.Column<string>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataTermino = table.Column<DateTime>(nullable: false),
                    Moedas = table.Column<int>(nullable: false),
                    IsOnline = table.Column<bool>(nullable: false),
                    QuantidadeMaxAlunos = table.Column<int>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missoes", x => x.MissaoId);
                    table.ForeignKey(
                        name: "FK_Missoes_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "TurmaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Presencas",
                columns: table => new
                {
                    PresencaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoPresenca = table.Column<int>(nullable: false),
                    MoedasGanha = table.Column<decimal>(nullable: false),
                    ChamadaId = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presencas", x => x.PresencaId);
                    table.ForeignKey(
                        name: "FK_Presencas_Pessoas_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Presencas_Chamadas_ChamadaId",
                        column: x => x.ChamadaId,
                        principalTable: "Chamadas",
                        principalColumn: "ChamadaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Respostas",
                columns: table => new
                {
                    RespostaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeId = table.Column<int>(nullable: false),
                    RespostaMissao = table.Column<string>(nullable: false),
                    Nota = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    MoedasGanhas = table.Column<decimal>(nullable: false),
                    MissaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respostas", x => x.RespostaId);
                    table.ForeignKey(
                        name: "FK_Respostas_Missoes_MissaoId",
                        column: x => x.MissaoId,
                        principalTable: "Missoes",
                        principalColumn: "MissaoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Respostas_Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "TimeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Moedas",
                columns: table => new
                {
                    MoedaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Motivo = table.Column<string>(nullable: false),
                    MoedasGanha = table.Column<decimal>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false),
                    RespostaId = table.Column<int>(nullable: true),
                    PresencaId = table.Column<int>(nullable: true),
                    AlunoDesafioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moedas", x => x.MoedaId);
                    table.ForeignKey(
                        name: "FK_Moedas_AlunoDesafios_AlunoDesafioId",
                        column: x => x.AlunoDesafioId,
                        principalTable: "AlunoDesafios",
                        principalColumn: "AlunoDesafioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Moedas_Pessoas_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Moedas_Presencas_PresencaId",
                        column: x => x.PresencaId,
                        principalTable: "Presencas",
                        principalColumn: "PresencaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Moedas_Respostas_RespostaId",
                        column: x => x.RespostaId,
                        principalTable: "Respostas",
                        principalColumn: "RespostaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Moedas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "TurmaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunoDesafios_AlunoId",
                table: "AlunoDesafios",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoDesafios_DesafioId",
                table: "AlunoDesafios",
                column: "DesafioId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoDesafios_TurmaId",
                table: "AlunoDesafios",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTimes_AlunoId",
                table: "AlunoTimes",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTimes_TimeId",
                table: "AlunoTimes",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTurmas_AlunoId",
                table: "AlunoTurmas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTurmas_TurmaId",
                table: "AlunoTurmas",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Chamadas_TurmaId",
                table: "Chamadas",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_InstituicaoId",
                table: "Cursos",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Missoes_TurmaId",
                table: "Missoes",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Moedas_AlunoDesafioId",
                table: "Moedas",
                column: "AlunoDesafioId");

            migrationBuilder.CreateIndex(
                name: "IX_Moedas_AlunoId",
                table: "Moedas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Moedas_PresencaId",
                table: "Moedas",
                column: "PresencaId");

            migrationBuilder.CreateIndex(
                name: "IX_Moedas_RespostaId",
                table: "Moedas",
                column: "RespostaId");

            migrationBuilder.CreateIndex(
                name: "IX_Moedas_TurmaId",
                table: "Moedas",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_PessoaId",
                table: "Notificacoes",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaCursos_CursoId",
                table: "PessoaCursos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaCursos_PessoaId",
                table: "PessoaCursos",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaInstituicoes_InstituicaoId",
                table: "PessoaInstituicoes",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaInstituicoes_PessoaId",
                table: "PessoaInstituicoes",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Presencas_AlunoId",
                table: "Presencas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Presencas_ChamadaId",
                table: "Presencas",
                column: "ChamadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Respostas_MissaoId",
                table: "Respostas",
                column: "MissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Respostas_TimeId",
                table: "Respostas",
                column: "TimeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_CursoId",
                table: "Turmas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_ProfessorId",
                table: "Turmas",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoTimes");

            migrationBuilder.DropTable(
                name: "AlunoTurmas");

            migrationBuilder.DropTable(
                name: "Moedas");

            migrationBuilder.DropTable(
                name: "Notificacoes");

            migrationBuilder.DropTable(
                name: "PessoaCursos");

            migrationBuilder.DropTable(
                name: "PessoaInstituicoes");

            migrationBuilder.DropTable(
                name: "AlunoDesafios");

            migrationBuilder.DropTable(
                name: "Presencas");

            migrationBuilder.DropTable(
                name: "Respostas");

            migrationBuilder.DropTable(
                name: "Desafios");

            migrationBuilder.DropTable(
                name: "Chamadas");

            migrationBuilder.DropTable(
                name: "Missoes");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Instituicoes");
        }
    }
}
