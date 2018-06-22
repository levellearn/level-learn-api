using LevelLearn.Domain.Institucional;
using LevelLearn.Service.Interfaces.Institucional;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LevelLearn.Web.Extensions.Services.Institucional
{
    public static class InstituocaoServiceExtensions
    {
        public static SelectList SelectListInstiuicoesAdmin(this IInstituicaoService instituicaoService, int pessoaId)
        {
            List<Instituicao> instituicoes = instituicaoService.InstituicoesAdmin(pessoaId);
            return new SelectList(instituicoes, "InstituicaoId", "Nome");
        }

        public static SelectList SelectListInstiuicoesProfessor(this IInstituicaoService instituicaoService, int pessoaId)
        {
            List<Instituicao> instituicoes = instituicaoService.InstituicoesProfessor(pessoaId);
            return new SelectList(instituicoes, "InstituicaoId", "Nome");
        }
    }
}
