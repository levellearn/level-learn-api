using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Resource;
using LevelLearn.Resource.Institucional;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Turma;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Turma turma = await _uow.Turmas.GetAsync(turmaId);

            if (turma == null)
                return ResultadoServiceFactory<Turma>.NotFound(_turmaResource.TurmaNaoEncontrada);

            return ResultadoServiceFactory<Turma>.Ok(turma);
        }

        public async Task<ResultadoService<IEnumerable<Turma>>> TurmasProfessorPorCurso(Guid cursoId, Guid pessoaId, FiltroPaginacao filtroPaginacao)
        {
            var turmas = await _uow.Turmas.TurmasProfessorPorCurso(cursoId, pessoaId, filtroPaginacao);

            var total = await _uow.Turmas.TotalTurmasProfessorPorCurso(cursoId, pessoaId, filtroPaginacao.FiltroPesquisa, filtroPaginacao.Ativo);

            return ResultadoServiceFactory<IEnumerable<Turma>>.Ok(turmas, total);
        }

        public async Task<ResultadoService<IEnumerable<Turma>>> TurmasAluno(Guid pessoaId, FiltroPaginacao filtroPaginacao)
        {
            var turmas = await _uow.Turmas.TurmasAluno(pessoaId, filtroPaginacao);

            var total = await _uow.Turmas.TotalTurmasAluno(pessoaId, filtroPaginacao.FiltroPesquisa, filtroPaginacao.Ativo);

            return ResultadoServiceFactory<IEnumerable<Turma>>.Ok(turmas, total);
        }

        public async Task<ResultadoService<Turma>> CadastrarTurma(Turma turma, Guid pessoaId)
        {
            // Validação objeto
            if (!turma.EstaValido())
                return ResultadoServiceFactory<Turma>.BadRequest(turma.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Validação BD
            if (!await _uow.Cursos.EntityExists(i => i.Id == turma.CursoId))
                return ResultadoServiceFactory<Turma>.BadRequest(_sharedResource.NaoEncontrado);

            bool professorDoCurso = await _uow.Cursos.ProfessorDoCurso(turma.CursoId, pessoaId);
            if (!professorDoCurso)
                return ResultadoServiceFactory<Turma>.Forbidden(_turmaResource.TurmaNaoPermitida);

            if (await TurmaExisteNoCurso(turma))
                return ResultadoServiceFactory<Turma>.BadRequest(_turmaResource.TurmaJaExiste);

            // Salva no BD
            await _uow.Turmas.AddAsync(turma);

            if (!await _uow.CommitAsync()) return ResultadoServiceFactory<Turma>.InternalServerError(_sharedResource.FalhaCadastrar);

            return ResultadoServiceFactory<Turma>.Created(turma, _sharedResource.CadastradoSucesso);
        }

        public async Task<ResultadoService<Turma>> EditarTurma(Guid turmaId, EditarTurmaVM turmaVM, Guid pessoaId)
        {
            // Validação BD
            Turma turma = await _uow.Turmas.GetAsync(turmaId);
            if (turma == null)
                return ResultadoServiceFactory<Turma>.NotFound(_turmaResource.TurmaNaoEncontrada);

            // Modifica objeto
            turma.Atualizar(turmaVM.Nome, turmaVM.Descricao, turmaVM.NomeDisciplina);

            // Validação objeto
            if (!turma.EstaValido())
                return ResultadoServiceFactory<Turma>.BadRequest(turma.DadosInvalidos(), _sharedResource.DadosInvalidos);

            bool professorDaTurma = turma.ProfessorId == pessoaId;
            if (!professorDaTurma)
                return ResultadoServiceFactory<Turma>.Forbidden(_turmaResource.TurmaNaoPermitida);

            // Validação BD
            if (await TurmaExisteNoCurso(turma))
                return ResultadoServiceFactory<Turma>.BadRequest(_turmaResource.TurmaJaExiste);

            // Salva no BD
            _uow.Turmas.Update(turma);

            if (!await _uow.CommitAsync()) return ResultadoServiceFactory<Turma>.InternalServerError(_sharedResource.FalhaAtualizar);

            return ResultadoServiceFactory<Turma>.NoContent(_sharedResource.AtualizadoSucesso);
        }

        public async Task<ResultadoService<Turma>> AlternarAtivacaoTurma(Guid turmaId, Guid pessoaId)
        {
            // Validação BD
            Turma turma = await _uow.Turmas.GetAsync(turmaId);

            if (turma == null)
                return ResultadoServiceFactory<Turma>.NotFound(_turmaResource.TurmaNaoEncontrada);

            bool professorDaTurma = turma.ProfessorId == pessoaId;
            if (!professorDaTurma)
                return ResultadoServiceFactory<Turma>.Forbidden(_turmaResource.TurmaNaoPermitida);

            if (turma.Ativo) turma.Desativar();
            else turma.Ativar();

            // Salva no BD
            _uow.Turmas.Update(turma);

            if (!await _uow.CommitAsync()) return ResultadoServiceFactory<Turma>.InternalServerError(_sharedResource.FalhaDeletar);

            return ResultadoServiceFactory<Turma>.NoContent(_sharedResource.DeletadoSucesso);
        }

        public async Task<ResultadoService<Turma>> IncluirAlunosNaTurma(Guid turmaId, Guid professorId, ICollection<Guid> idsAluno)
        {
            // Validação BD
            Turma turma = await _uow.Turmas.TurmaCompleta(turmaId, asNoTracking: false);

            if (turma == null) return ResultadoServiceFactory<Turma>.NotFound(_turmaResource.TurmaNaoEncontrada);

            bool professorDaTurma = turma.ProfessorId == professorId;
            if (!professorDaTurma) return ResultadoServiceFactory<Turma>.Forbidden(_turmaResource.TurmaNaoPermitida);

            // Modifica objeto
            List<AlunoTurma> alunosTurma = idsAluno.Select(idAluno => new AlunoTurma(idAluno, turmaId)).ToList();
            // TODO: Verificar aluno existe
            //var idsAlunosNaoIncluidos = idsAluno.Except(turma.Alunos.Select(a => a.AlunoId));
            turma.AtribuirAlunos(alunosTurma);

            // Validação objeto
            if (!turma.EstaValido())
                return ResultadoServiceFactory<Turma>.BadRequest(turma.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Salva no BD
            _uow.Turmas.Update(turma);
            if (!await _uow.CommitAsync()) return ResultadoServiceFactory<Turma>.InternalServerError(_sharedResource.FalhaAtualizar);

            return ResultadoServiceFactory<Turma>.NoContent(_sharedResource.AtualizadoSucesso);
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
