using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LevelLearn.Infra.EFCore.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedeIntituicao",
                table: "Instituicoes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Turmas",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 352, DateTimeKind.Utc).AddTicks(8730),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 833, DateTimeKind.Utc).AddTicks(8881));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Pessoas",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 388, DateTimeKind.Utc).AddTicks(1512),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 860, DateTimeKind.Utc).AddTicks(1300));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "PessoaInstituicoes",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 390, DateTimeKind.Utc).AddTicks(8128),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 862, DateTimeKind.Utc).AddTicks(2630));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "PessoaCursos",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 393, DateTimeKind.Utc).AddTicks(3719),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 864, DateTimeKind.Utc).AddTicks(1159));

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Instituicoes",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Instituicoes",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 342, DateTimeKind.Utc).AddTicks(5831),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 825, DateTimeKind.Utc).AddTicks(7565));

            migrationBuilder.AddColumn<int>(
                name: "Rede",
                table: "Instituicoes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Cursos",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 348, DateTimeKind.Utc).AddTicks(2408),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 830, DateTimeKind.Utc).AddTicks(3912));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "AlunoTurmas",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 395, DateTimeKind.Utc).AddTicks(6110),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 865, DateTimeKind.Utc).AddTicks(8997));

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_Ativo",
                table: "Turmas",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_Ativo",
                table: "Pessoas",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "IX_Instituicoes_Ativo",
                table: "Instituicoes",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_Ativo",
                table: "Cursos",
                column: "Ativo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Turmas_Ativo",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_Ativo",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Instituicoes_Ativo",
                table: "Instituicoes");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_Ativo",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "Rede",
                table: "Instituicoes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Turmas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 833, DateTimeKind.Utc).AddTicks(8881),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 352, DateTimeKind.Utc).AddTicks(8730));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Pessoas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 860, DateTimeKind.Utc).AddTicks(1300),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 388, DateTimeKind.Utc).AddTicks(1512));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "PessoaInstituicoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 862, DateTimeKind.Utc).AddTicks(2630),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 390, DateTimeKind.Utc).AddTicks(8128));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "PessoaCursos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 864, DateTimeKind.Utc).AddTicks(1159),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 393, DateTimeKind.Utc).AddTicks(3719));

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Instituicoes",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Instituicoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 825, DateTimeKind.Utc).AddTicks(7565),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 342, DateTimeKind.Utc).AddTicks(5831));

            migrationBuilder.AddColumn<int>(
                name: "RedeIntituicao",
                table: "Instituicoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Cursos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 830, DateTimeKind.Utc).AddTicks(3912),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 348, DateTimeKind.Utc).AddTicks(2408));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "AlunoTurmas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 11, 18, 1, 44, 865, DateTimeKind.Utc).AddTicks(8997),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 6, 15, 20, 31, 23, 395, DateTimeKind.Utc).AddTicks(6110));
        }
    }
}
