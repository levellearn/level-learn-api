﻿using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Instituicao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Institucional
{
    public interface ICursoService : IServiceBase<Curso, Guid>, IDisposable
    {
        Task<ResultadoService<IEnumerable<Curso>>> CursosProfessorPorInstituicao(Guid instituicaoId, Guid pessoaId, FiltroPaginacao filtroPaginacao);
        Task<ResultadoService<Curso>> ObterCurso(Guid cursoId, Guid pessoaId);
        Task<ResultadoService<Curso>> CadastrarCurso(Curso curso, Guid pessoaId);
        Task<ResultadoService<Curso>> EditarCurso(Guid cursoId, EditarCursoVM cursoVM, Guid pessoaId);
        Task<ResultadoService<Curso>> AlternarAtivacaoCurso(Guid cursoId, Guid pessoaId);
    }
}
