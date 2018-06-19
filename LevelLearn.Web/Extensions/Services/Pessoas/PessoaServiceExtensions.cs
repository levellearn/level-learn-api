using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Pessoas;
using LevelLearn.Service.Interfaces.Pessoas;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LevelLearn.Web.Extensions.Services.Pessoas
{
    public static class PessoaServiceExtensions
    {
        public static SelectList SelectListProfessores(this IPessoaService service)
        {
            List<Pessoa> pessoas = service.Select(p => p.TipoPessoa == TipoPessoaEnum.Professor);

            return new SelectList(pessoas, "PessoaId", "Nome");
        }

        public static SelectList SelectListAlunos(this IPessoaService service)
        {
            List<Pessoa> pessoas = service.Select(p => p.TipoPessoa == TipoPessoaEnum.Aluno);

            return new SelectList(pessoas, "PessoaId", "Nome");
        }

        public static SelectList SelectListProfessoresWithoutUser(this IPessoaService service, int userId)
        {
            List<Pessoa> pessoas = service.Select(p => p.TipoPessoa == TipoPessoaEnum.Professor);

            pessoas.RemoveAll(p => p.PessoaId == userId);

            return new SelectList(pessoas, "PessoaId", "Nome");
        }

        public static SelectList SelectListAlunosWithoutUser(this IPessoaService service, int userId)
        {
            List<Pessoa> pessoas = service.Select(p => p.TipoPessoa == TipoPessoaEnum.Aluno);

            pessoas.RemoveAll(p => p.PessoaId == userId);

            return new SelectList(pessoas, "PessoaId", "Nome");
        }
    }
}
