﻿// <auto-generated />
using System;
using LevelLearn.Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LevelLearn.Infra.EFCore.Migrations
{
    [DbContext(typeof(LevelLearnContext))]
    partial class LevelLearnContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LevelLearn.Domain.Entities.Institucional.Curso", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2020, 6, 10, 19, 0, 29, 242, DateTimeKind.Utc).AddTicks(6447));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<Guid>("InstituicaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NomePesquisa")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("InstituicaoId");

                    b.HasIndex("NomePesquisa");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Institucional.Instituicao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<int>("CategoriaAdministrativa")
                        .HasColumnType("int");

                    b.Property<string>("Cep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cnpj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2020, 6, 10, 19, 0, 29, 235, DateTimeKind.Utc).AddTicks(8699));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("Municipio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NivelEnsino")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NomePesquisa")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("OrganizacaoAcademica")
                        .HasColumnType("int");

                    b.Property<int>("Rede")
                        .HasColumnType("int");

                    b.Property<string>("Sigla")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UF")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NomePesquisa");

                    b.ToTable("Instituicoes");
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Institucional.Turma", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<Guid>("CursoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2020, 6, 10, 19, 0, 29, 247, DateTimeKind.Utc).AddTicks(7732));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<double>("Meta")
                        .HasColumnType("float");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NomeDisciplina")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NomePesquisa")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<Guid?>("PessoaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProfessorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("NomePesquisa");

                    b.HasIndex("PessoaId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Pessoas.AlunoTurma", b =>
                {
                    b.Property<Guid>("AlunoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TurmaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2020, 6, 10, 19, 0, 29, 295, DateTimeKind.Utc).AddTicks(8419));

                    b.HasKey("AlunoId", "TurmaId");

                    b.HasIndex("TurmaId");

                    b.ToTable("AlunoTurmas");
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Pessoas.Pessoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2020, 6, 10, 19, 0, 29, 287, DateTimeKind.Utc).AddTicks(5179));

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genero")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("NomePesquisa")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("TipoPessoa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NomePesquisa");

                    b.ToTable("Pessoas");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Pessoa");
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Pessoas.PessoaCurso", b =>
                {
                    b.Property<Guid>("PessoaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CursoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2020, 6, 10, 19, 0, 29, 293, DateTimeKind.Utc).AddTicks(4110));

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.HasKey("PessoaId", "CursoId");

                    b.HasIndex("CursoId");

                    b.ToTable("PessoaCursos");
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Pessoas.PessoaInstituicao", b =>
                {
                    b.Property<Guid>("PessoaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InstituicaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2020, 6, 10, 19, 0, 29, 290, DateTimeKind.Utc).AddTicks(6397));

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.HasKey("PessoaId", "InstituicaoId");

                    b.HasIndex("InstituicaoId");

                    b.ToTable("PessoaInstituicoes");
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Usuarios.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ImagemNome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagemUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PessoaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PessoaId")
                        .IsUnique();

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Pessoas.Admin", b =>
                {
                    b.HasBaseType("LevelLearn.Domain.Entities.Pessoas.Pessoa");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Pessoas.Aluno", b =>
                {
                    b.HasBaseType("LevelLearn.Domain.Entities.Pessoas.Pessoa");

                    b.Property<string>("RA")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Aluno");
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Pessoas.Professor", b =>
                {
                    b.HasBaseType("LevelLearn.Domain.Entities.Pessoas.Pessoa");

                    b.HasDiscriminator().HasValue("Professor");
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Institucional.Curso", b =>
                {
                    b.HasOne("LevelLearn.Domain.Entities.Institucional.Instituicao", "Instituicao")
                        .WithMany("Cursos")
                        .HasForeignKey("InstituicaoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Institucional.Turma", b =>
                {
                    b.HasOne("LevelLearn.Domain.Entities.Institucional.Curso", "Curso")
                        .WithMany("Turmas")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LevelLearn.Domain.Entities.Pessoas.Pessoa", null)
                        .WithMany("Turmas")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LevelLearn.Domain.Entities.Pessoas.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Pessoas.AlunoTurma", b =>
                {
                    b.HasOne("LevelLearn.Domain.Entities.Pessoas.Pessoa", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LevelLearn.Domain.Entities.Institucional.Turma", "Turma")
                        .WithMany("Alunos")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Pessoas.Pessoa", b =>
                {
                    b.OwnsOne("LevelLearn.Domain.ValueObjects.CPF", "Cpf", b1 =>
                        {
                            b1.Property<Guid>("PessoaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Numero")
                                .HasColumnName("CPF")
                                .HasColumnType("varchar(11)");

                            b1.HasKey("PessoaId");

                            b1.ToTable("Pessoas");

                            b1.WithOwner()
                                .HasForeignKey("PessoaId");
                        });

                    b.OwnsOne("LevelLearn.Domain.ValueObjects.Celular", "Celular", b1 =>
                        {
                            b1.Property<Guid>("PessoaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Numero")
                                .HasColumnName("Celular")
                                .HasColumnType("varchar(14)");

                            b1.HasKey("PessoaId");

                            b1.ToTable("Pessoas");

                            b1.WithOwner()
                                .HasForeignKey("PessoaId");
                        });

                    b.OwnsOne("LevelLearn.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("PessoaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Endereco")
                                .HasColumnName("Email")
                                .HasColumnType("varchar(190)");

                            b1.HasKey("PessoaId");

                            b1.ToTable("Pessoas");

                            b1.WithOwner()
                                .HasForeignKey("PessoaId");
                        });
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Pessoas.PessoaCurso", b =>
                {
                    b.HasOne("LevelLearn.Domain.Entities.Institucional.Curso", "Curso")
                        .WithMany("Pessoas")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LevelLearn.Domain.Entities.Pessoas.Pessoa", "Pessoa")
                        .WithMany("Cursos")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Pessoas.PessoaInstituicao", b =>
                {
                    b.HasOne("LevelLearn.Domain.Entities.Institucional.Instituicao", "Instituicao")
                        .WithMany("Pessoas")
                        .HasForeignKey("InstituicaoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LevelLearn.Domain.Entities.Pessoas.Pessoa", "Pessoa")
                        .WithMany("Instituicoes")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("LevelLearn.Domain.Entities.Usuarios.Usuario", b =>
                {
                    b.HasOne("LevelLearn.Domain.Entities.Pessoas.Pessoa", "Pessoa")
                        .WithOne()
                        .HasForeignKey("LevelLearn.Domain.Entities.Usuarios.Usuario", "PessoaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LevelLearn.Domain.Entities.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LevelLearn.Domain.Entities.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LevelLearn.Domain.Entities.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LevelLearn.Domain.Entities.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
