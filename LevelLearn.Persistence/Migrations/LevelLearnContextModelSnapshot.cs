﻿// <auto-generated />
using System;
using LevelLearn.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LevelLearn.Persistence.Migrations
{
    [DbContext(typeof(LevelLearnContext))]
    partial class LevelLearnContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LevelLearn.Domain.Institucional.Curso", b =>
                {
                    b.Property<int>("CursoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao");

                    b.Property<int>("InstituicaoId");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("CursoId");

                    b.HasIndex("InstituicaoId");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("LevelLearn.Domain.Institucional.Instituicao", b =>
                {
                    b.Property<int>("InstituicaoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("InstituicaoId");

                    b.ToTable("Instituicoes");
                });

            modelBuilder.Entity("LevelLearn.Domain.Institucional.Turma", b =>
                {
                    b.Property<int>("TurmaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CursoId");

                    b.Property<string>("Descricao");

                    b.Property<decimal>("Meta");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<int>("ProfessorId");

                    b.HasKey("TurmaId");

                    b.HasIndex("CursoId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("LevelLearn.Domain.Jogo.Chamada", b =>
                {
                    b.Property<int>("ChamadaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataAula");

                    b.Property<int>("Periodo");

                    b.Property<int>("TurmaId");

                    b.HasKey("ChamadaId");

                    b.HasIndex("TurmaId");

                    b.ToTable("Chamadas");
                });

            modelBuilder.Entity("LevelLearn.Domain.Jogo.Desafio", b =>
                {
                    b.Property<int>("DesafioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Imagem")
                        .IsRequired();

                    b.Property<bool>("IsCompletaUmaVez");

                    b.Property<decimal>("Moedas");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<int>("Pedra");

                    b.HasKey("DesafioId");

                    b.ToTable("Desafios");
                });

            modelBuilder.Entity("LevelLearn.Domain.Jogo.Missao", b =>
                {
                    b.Property<int>("MissaoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataInicio");

                    b.Property<DateTime>("DataTermino");

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<bool>("IsOnline");

                    b.Property<int>("Moedas");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Objetivo")
                        .IsRequired();

                    b.Property<int>("QuantidadeMaxAlunos");

                    b.Property<int>("TurmaId");

                    b.HasKey("MissaoId");

                    b.HasIndex("TurmaId");

                    b.ToTable("Missoes");
                });

            modelBuilder.Entity("LevelLearn.Domain.Jogo.Moeda", b =>
                {
                    b.Property<int>("MoedaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlunoDesafioId");

                    b.Property<int>("AlunoId");

                    b.Property<DateTime>("DataCadastro");

                    b.Property<decimal>("MoedasGanha");

                    b.Property<string>("Motivo")
                        .IsRequired();

                    b.Property<int?>("PresencaId");

                    b.Property<int?>("RespostaId");

                    b.Property<int>("TurmaId");

                    b.HasKey("MoedaId");

                    b.HasIndex("AlunoDesafioId");

                    b.HasIndex("AlunoId");

                    b.HasIndex("PresencaId");

                    b.HasIndex("RespostaId");

                    b.HasIndex("TurmaId");

                    b.ToTable("Moedas");
                });

            modelBuilder.Entity("LevelLearn.Domain.Jogo.Presenca", b =>
                {
                    b.Property<int>("PresencaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlunoId");

                    b.Property<int>("ChamadaId");

                    b.Property<decimal>("MoedasGanha");

                    b.Property<int>("TipoPresenca");

                    b.HasKey("PresencaId");

                    b.HasIndex("AlunoId");

                    b.HasIndex("ChamadaId");

                    b.ToTable("Presencas");
                });

            modelBuilder.Entity("LevelLearn.Domain.Jogo.Resposta", b =>
                {
                    b.Property<int>("RespostaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MissaoId");

                    b.Property<decimal>("MoedasGanhas");

                    b.Property<decimal>("Nota");

                    b.Property<string>("RespostaMissao")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.Property<int>("TimeId");

                    b.HasKey("RespostaId");

                    b.HasIndex("MissaoId");

                    b.HasIndex("TimeId")
                        .IsUnique();

                    b.ToTable("Respostas");
                });

            modelBuilder.Entity("LevelLearn.Domain.Jogo.Time", b =>
                {
                    b.Property<int>("TimeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("TimeId");

                    b.ToTable("Times");
                });

            modelBuilder.Entity("LevelLearn.Domain.Pessoas.AlunoDesafio", b =>
                {
                    b.Property<int>("AlunoDesafioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlunoId");

                    b.Property<DateTime>("DataCadastro");

                    b.Property<int>("DesafioId");

                    b.Property<decimal>("MoedasGanha");

                    b.Property<int>("TurmaId");

                    b.HasKey("AlunoDesafioId");

                    b.HasIndex("AlunoId");

                    b.HasIndex("DesafioId");

                    b.HasIndex("TurmaId");

                    b.ToTable("AlunoDesafios");
                });

            modelBuilder.Entity("LevelLearn.Domain.Pessoas.AlunoTime", b =>
                {
                    b.Property<int>("AlunoTimeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlunoId");

                    b.Property<bool>("IsCriador");

                    b.Property<int>("TimeId");

                    b.HasKey("AlunoTimeId");

                    b.HasIndex("AlunoId");

                    b.HasIndex("TimeId");

                    b.ToTable("AlunoTimes");
                });

            modelBuilder.Entity("LevelLearn.Domain.Pessoas.AlunoTurma", b =>
                {
                    b.Property<int>("AlunoTurmaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlunoId");

                    b.Property<int>("TurmaId");

                    b.HasKey("AlunoTurmaId");

                    b.HasIndex("AlunoId");

                    b.HasIndex("TurmaId");

                    b.ToTable("AlunoTurmas");
                });

            modelBuilder.Entity("LevelLearn.Domain.Pessoas.Notificacao", b =>
                {
                    b.Property<int>("NotificacaoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCadastro");

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<bool>("IsVisualizada");

                    b.Property<string>("Link");

                    b.Property<int>("PessoaId");

                    b.Property<string>("Titulo")
                        .IsRequired();

                    b.HasKey("NotificacaoId");

                    b.HasIndex("PessoaId");

                    b.ToTable("Notificacoes");
                });

            modelBuilder.Entity("LevelLearn.Domain.Pessoas.Pessoa", b =>
                {
                    b.Property<int>("PessoaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCadastro");

                    b.Property<DateTime?>("DataNascimento");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Imagem")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("RA");

                    b.Property<int>("Sexo");

                    b.Property<int>("TipoPessoa");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("PessoaId");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("LevelLearn.Domain.Pessoas.PessoaCurso", b =>
                {
                    b.Property<int>("PessoaCursoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CursoId");

                    b.Property<int>("Perfil");

                    b.Property<int>("PessoaId");

                    b.HasKey("PessoaCursoId");

                    b.HasIndex("CursoId");

                    b.HasIndex("PessoaId");

                    b.ToTable("PessoaCursos");
                });

            modelBuilder.Entity("LevelLearn.Domain.Pessoas.PessoaInstituicao", b =>
                {
                    b.Property<int>("PessoaInstituicaoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InstituicaoId");

                    b.Property<int>("Perfil");

                    b.Property<int>("PessoaId");

                    b.HasKey("PessoaInstituicaoId");

                    b.HasIndex("InstituicaoId");

                    b.HasIndex("PessoaId");

                    b.ToTable("PessoaInstituicoes");
                });

            modelBuilder.Entity("LevelLearn.Domain.Institucional.Curso", b =>
                {
                    b.HasOne("LevelLearn.Domain.Institucional.Instituicao", "Instituicao")
                        .WithMany()
                        .HasForeignKey("InstituicaoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LevelLearn.Domain.Institucional.Turma", b =>
                {
                    b.HasOne("LevelLearn.Domain.Institucional.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Pessoas.Pessoa", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LevelLearn.Domain.Jogo.Chamada", b =>
                {
                    b.HasOne("LevelLearn.Domain.Institucional.Turma", "Turma")
                        .WithMany()
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LevelLearn.Domain.Jogo.Missao", b =>
                {
                    b.HasOne("LevelLearn.Domain.Institucional.Turma", "Turma")
                        .WithMany()
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LevelLearn.Domain.Jogo.Moeda", b =>
                {
                    b.HasOne("LevelLearn.Domain.Pessoas.AlunoDesafio", "AlunoDesafio")
                        .WithMany()
                        .HasForeignKey("AlunoDesafioId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Pessoas.Pessoa", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Jogo.Presenca", "Presenca")
                        .WithMany()
                        .HasForeignKey("PresencaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Jogo.Resposta", "Resposta")
                        .WithMany()
                        .HasForeignKey("RespostaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Institucional.Turma", "Turma")
                        .WithMany()
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LevelLearn.Domain.Jogo.Presenca", b =>
                {
                    b.HasOne("LevelLearn.Domain.Pessoas.Pessoa", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Jogo.Chamada", "Chamada")
                        .WithMany()
                        .HasForeignKey("ChamadaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LevelLearn.Domain.Jogo.Resposta", b =>
                {
                    b.HasOne("LevelLearn.Domain.Jogo.Missao", "Missao")
                        .WithMany()
                        .HasForeignKey("MissaoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Jogo.Time", "Time")
                        .WithOne("Resposta")
                        .HasForeignKey("LevelLearn.Domain.Jogo.Resposta", "TimeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LevelLearn.Domain.Pessoas.AlunoDesafio", b =>
                {
                    b.HasOne("LevelLearn.Domain.Pessoas.Pessoa", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Jogo.Desafio", "Desafio")
                        .WithMany()
                        .HasForeignKey("DesafioId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Institucional.Turma", "Turma")
                        .WithMany()
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LevelLearn.Domain.Pessoas.AlunoTime", b =>
                {
                    b.HasOne("LevelLearn.Domain.Pessoas.Pessoa", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Jogo.Time", "Time")
                        .WithMany()
                        .HasForeignKey("TimeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LevelLearn.Domain.Pessoas.AlunoTurma", b =>
                {
                    b.HasOne("LevelLearn.Domain.Pessoas.Pessoa", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Institucional.Turma", "Turma")
                        .WithMany("Alunos")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LevelLearn.Domain.Pessoas.Notificacao", b =>
                {
                    b.HasOne("LevelLearn.Domain.Pessoas.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LevelLearn.Domain.Pessoas.PessoaCurso", b =>
                {
                    b.HasOne("LevelLearn.Domain.Institucional.Curso", "Curso")
                        .WithMany("Pessoas")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Pessoas.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LevelLearn.Domain.Pessoas.PessoaInstituicao", b =>
                {
                    b.HasOne("LevelLearn.Domain.Institucional.Instituicao", "Instituicao")
                        .WithMany("Pessoas")
                        .HasForeignKey("InstituicaoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Pessoas.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
