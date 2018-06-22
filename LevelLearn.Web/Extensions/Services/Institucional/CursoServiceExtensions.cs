using LevelLearn.Domain.Institucional;
using LevelLearn.Service.Interfaces.Institucional;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LevelLearn.Web.Extensions.Services.Institucional
{
    public static class CursoServiceExtensions
    {
        public static SelectList SelectListCursosProfessor(this ICursoService cursoService, int professorId)
        {
            List<Curso> cursos = cursoService.CursosProfessor(professorId);
            return new SelectList(cursos, "CursoId", "Nome");
        }
    }
}
