using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Resource;
using LevelLearn.Resource.Institucional;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Instituicao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Institucional
{
    public class CursoService : ServiceBase<Curso, Guid>, ICursoService
    {
        #region Ctor

        private readonly IUnitOfWork _uow;
        private readonly ISharedResource _sharedResource;
        private readonly CursoResource _cursoResource;

        public CursoService(IUnitOfWork uow, ISharedResource sharedResource)
            : base(uow.Cursos)
        {
            _uow = uow;
            _sharedResource = sharedResource;
            _cursoResource = new CursoResource();
        }

        #endregion

        public async Task<ResultadoService<IEnumerable<Curso>>> CursosProfessorPorInstituicao(Guid instituicaoId, Guid pessoaId, FiltroPaginacao filtroPaginacao)
        {
            var cursos = await _uow.Cursos.CursosProfessorPorInstituicao(instituicaoId, pessoaId, filtroPaginacao);

            int total = await _uow.Cursos.TotalCursosProfessorPorInstituicao(instituicaoId, pessoaId, filtroPaginacao.FiltroPesquisa, filtroPaginacao.Ativo);

            return ResultadoServiceFactory<IEnumerable<Curso>>.Ok(cursos, total);
        }

        public async Task<ResultadoService<Curso>> ObterCurso(Guid cursoId, Guid pessoaId)
        {
            Curso curso = await _uow.Cursos.CursoCompleto(cursoId);

            if (curso == null)
                return ResultadoServiceFactory<Curso>.NotFound(_cursoResource.CursoNaoEncontrado);

            return ResultadoServiceFactory<Curso>.Ok(curso);
        }

        public async Task<ResultadoService<Curso>> CadastrarCurso(Curso curso, Guid pessoaId)
        {
            // Validação objeto
            if (!curso.EstaValido())
                return ResultadoServiceFactory<Curso>.BadRequest(curso.DadosInvalidos(), _sharedResource.DadosInvalidos);

            curso.AtribuirPessoa(new PessoaCurso(TipoPessoa.Professor, pessoaId, curso.Id));

            // Validação BD
            if (!await _uow.Instituicoes.EntityExists(i => i.Id == curso.InstituicaoId))
                return ResultadoServiceFactory<Curso>.BadRequest(_sharedResource.NaoEncontrado);

            bool professorDaInstituicao = await _uow.Instituicoes.PertenceInstituicao(curso.InstituicaoId, pessoaId);
            if (!professorDaInstituicao)
                return ResultadoServiceFactory<Curso>.Forbidden(_cursoResource.CursoNaoPermitido);

            if (await CursoExisteNaInstituicao(curso))
                return ResultadoServiceFactory<Curso>.BadRequest(_cursoResource.CursoJaExiste);

            // Salva no BD
            await _uow.Cursos.AddAsync(curso);

            if (!await _uow.CompleteAsync()) return ResultadoServiceFactory<Curso>.InternalServerError(_sharedResource.FalhaCadastrar);

            return ResultadoServiceFactory<Curso>.Created(curso, _sharedResource.CadastradoSucesso);
        }

        public async Task<ResultadoService<Curso>> EditarCurso(Guid cursoId, EditarCursoVM cursoVM, Guid pessoaId)
        {
            // Validação BD
            Curso curso = await _uow.Cursos.GetAsync(cursoId);
            if (curso == null)
                return ResultadoServiceFactory<Curso>.NotFound(_cursoResource.CursoNaoEncontrado);

            // Modifica objeto
            curso.Atualizar(cursoVM.Nome, cursoVM.Sigla, cursoVM.Descricao);

            // Validação objeto
            if (!curso.EstaValido())
                return ResultadoServiceFactory<Curso>.BadRequest(curso.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Validação BD
            // TODO: Refatorar para pegar do objeto
            bool professorDoCurso = await _uow.Cursos.ProfessorDoCurso(cursoId, pessoaId);
            if (!professorDoCurso)
                return ResultadoServiceFactory<Curso>.Forbidden(_cursoResource.CursoNaoPermitido);

            if (await CursoExisteNaInstituicao(curso))
                return ResultadoServiceFactory<Curso>.BadRequest(_cursoResource.CursoJaExiste);

            // Salva no BD
            _uow.Cursos.Update(curso);

            if (!await _uow.CompleteAsync()) return ResultadoServiceFactory<Curso>.InternalServerError(_sharedResource.FalhaAtualizar);

            return ResultadoServiceFactory<Curso>.NoContent(_sharedResource.AtualizadoSucesso);
        }

        public async Task<ResultadoService<Curso>> AlternarAtivacaoCurso(Guid cursoId, Guid pessoaId)
        {
            // Validação BD
            Curso curso = await _uow.Cursos.CursoCompleto(cursoId);

            if (curso == null)
                return ResultadoServiceFactory<Curso>.NotFound(_cursoResource.CursoNaoEncontrado);

            // TODO: Refatorar para pegar do objeto
            bool professorDoCurso = await _uow.Cursos.ProfessorDoCurso(cursoId, pessoaId);
            if (!professorDoCurso)
                return ResultadoServiceFactory<Curso>.Forbidden(_cursoResource.CursoNaoPermitido);

            if (curso.Ativo) curso.Desativar();
            else curso.Ativar();

            // Salva no BD
            _uow.Cursos.Update(curso);

            if (!await _uow.CompleteAsync()) return ResultadoServiceFactory<Curso>.InternalServerError(_sharedResource.FalhaDeletar);

            return ResultadoServiceFactory<Curso>.NoContent(_sharedResource.DeletadoSucesso);
        }

        /// <summary>
        /// Verifica se está tentando atualizar para um curso que já existe
        /// </summary>
        /// <param name="curso">Curso a ser verificado</param>
        /// <returns></returns>
        private async Task<bool> CursoExisteNaInstituicao(Curso curso)
        {
            bool cursoExiste = await _uow.Cursos.EntityExists(c =>
                c.NomePesquisa == curso.NomePesquisa &&
                c.Id != curso.Id &&
                c.InstituicaoId == curso.InstituicaoId);

            return cursoExiste;
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}
