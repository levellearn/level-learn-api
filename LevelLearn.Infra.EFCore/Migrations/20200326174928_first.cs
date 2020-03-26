using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LevelLearn.Infra.EFCore.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Instituicoes",
                keyColumn: "Id",
                keyValue: new Guid("bf3957e3-54f8-42fd-9103-c8c4dfa9aa40"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Instituicoes",
                columns: new[] { "Id", "Ativo", "DataCadastro", "Descricao", "Nome", "NomePesquisa" },
                values: new object[] { new Guid("bf3957e3-54f8-42fd-9103-c8c4dfa9aa40"), true, new DateTime(2020, 3, 23, 12, 52, 9, 584, DateTimeKind.Local).AddTicks(6156), "Descrição Teste", "Instituição Teste", "instituicaoteste" });
        }
    }
}
