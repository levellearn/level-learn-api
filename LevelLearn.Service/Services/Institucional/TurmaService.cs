using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Resource;
using LevelLearn.Resource.Institucional;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Turma;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Institucional
{
    public class TurmaService : ServiceBase<Turma, Guid>, ITurmaService
    {
        #region Ctor

        private readonly IUnitOfWork _uow;
        private readonly ISharedResource _sharedResource;
        private readonly TurmaResource _turmaResource;

        public TurmaService(IUnitOfWork uow, ISharedResource sharedResource)
            : base(uow.Turmas)
        {
            _uow = uow;
            _sharedResource = sharedResource;
            _turmaResource = new TurmaResource();
        }

        #endregion

        public async Task<ResultadoService<Turma>> ObterTurma(Guid turmaId, Guid pessoaId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultadoService<IEnumerable<Turma>>> TurmasCursoProfessor(Guid cursoId, Guid pessoaId, FiltroPaginacao filtroPaginacao)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultadoService<IEnumerable<Turma>>> TurmasCursoAluno(Guid cursoId, Guid pessoaId, FiltroPaginacao filtroPaginacao)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultadoService<Turma>> CadastrarTurma(Turma turma, Guid pessoaId)
        {
            // Validação objeto
            if (!turma.EstaValido())
                return ResultadoServiceFactory<Turma>.BadRequest(turma.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Validação BD
            bool professorDoCurso = await _uow.Cursos.ProfessorDoCurso(turma.CursoId, pessoaId);
            if (!professorDoCurso)
                return ResultadoServiceFactory<Turma>.Forbidden(_turmaResource.TurmaNaoPermitida);

            if (!await _uow.Cursos.EntityExists(i => i.Id == turma.CursoId))
                return ResultadoServiceFactory<Turma>.BadRequest(_sharedResource.NaoEncontrado);

            if (await TurmaExisteNoCurso(turma))
                return ResultadoServiceFactory<Turma>.BadRequest(_turmaResource.TurmaJaExiste);

            // Salva no BD
            await _uow.Turmas.AddAsync(turma);

            if (!await _uow.CompleteAsync()) return ResultadoServiceFactory<Turma>.InternalServerError(_sharedResource.FalhaCadastrar);

            return ResultadoServiceFactory<Turma>.Created(turma, _sharedResource.CadastradoSucesso);
        }

        public async Task<ResultadoService<Turma>> EditarTurma(Guid turmaId, EditarTurmaVM turmaVM, Guid pessoaId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultadoService<Turma>> AlternarAtivacaoTurma(Guid turmaId, Guid pessoaId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

        /// <summary>
        /// Verifica se está tentando atualizar para uma turma que já existe
        /// </summary>
        /// <param name="turma">Turma a ser verificado</param>
        /// <returns></returns>
        private async Task<bool> TurmaExisteNoCurso(Turma turma)
        {
            bool turmaExiste = await _uow.Turmas.EntityExists(t =>
                t.NomePesquisa == turma.NomePesquisa &&
                t.Id != turma.Id &&
                t.CursoId == turma.CursoId);

            return turmaExiste;
        }

    }
}
