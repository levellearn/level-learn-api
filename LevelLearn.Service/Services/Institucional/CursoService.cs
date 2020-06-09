using LevelLearn.Domain.Entities.Comum;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Resource;
using LevelLearn.Resource.Institucional;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Curso;
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

        public async Task<ResponseAPI<IEnumerable<Curso>>> CursosInstituicaoProfessor(Guid instituicaoId, Guid pessoaId, FiltroPaginacao filtroPaginacao)
        {
            var cursos = await _uow.Cursos.CursosInstituicaoProfessor(instituicaoId, pessoaId, filtroPaginacao);

            var total = await _uow.Cursos.TotalCursosInstituicaoProfessor(instituicaoId, pessoaId, filtroPaginacao.FiltroPesquisa, filtroPaginacao.Ativo);

            return ResponseFactory<IEnumerable<Curso>>.Ok(cursos, total);
        }

        public async Task<ResponseAPI<Curso>> ObterCurso(Guid cursoId, Guid pessoaId)
        {
            Curso curso = await _uow.Cursos.CursoCompleto(cursoId);

            if (curso == null)
                return ResponseFactory<Curso>.NotFound(_cursoResource.CursoNaoEncontrado);

            return ResponseFactory<Curso>.Ok(curso);
        }

        public async Task<ResponseAPI<Curso>> CadastrarCurso(CadastrarCursoVM cursoVM, Guid pessoaId)
        {
            // TODO: Somente pode criar cursos um professor Admin?
            //bool professorAdmin = await _uow.Instituicoes.ProfessorAdmin(cursoVM.InstituicaoId, pessoaId);
            //if (!professorAdmin)
            //   return ResponseFactory<Curso>.Forbidden(_sharedLocalizer.InstituicaoNaoPermitida);

            var curso = new Curso(cursoVM.Nome, cursoVM.Sigla, cursoVM.Descricao, cursoVM.InstituicaoId);

            // Validação objeto
            if (!curso.EstaValido())
                return ResponseFactory<Curso>.BadRequest(curso.DadosInvalidos(), _sharedResource.DadosInvalidos);

            if (!await _uow.Instituicoes.EntityExists(i => i.Id == curso.InstituicaoId))
                return ResponseFactory<Curso>.BadRequest(_sharedResource.NaoEncontrado);

            var pessoaCurso = new PessoaCurso(TiposPessoa.Professor, pessoaId, curso.Id);
            curso.AtribuirPessoa(pessoaCurso);

            // Validação BD
            if (await CursoExisteNaInstituicao(curso))
                return ResponseFactory<Curso>.BadRequest(_cursoResource.CursoJaExiste);

            // Salva no BD
            await _uow.Cursos.AddAsync(curso);

            if (!await _uow.CompleteAsync()) return ResponseFactory<Curso>.InternalServerError(_sharedResource.FalhaCadastrar);

            return ResponseFactory<Curso>.Created(curso, _sharedResource.CadastradoSucesso);
        }

        public async Task<ResponseAPI<Curso>> EditarCurso(Guid cursoId, EditarCursoVM cursoVM, Guid pessoaId)
        {
            Curso curso = await _uow.Cursos.GetAsync(cursoId);

            if (curso == null)
                return ResponseFactory<Curso>.NotFound(_cursoResource.CursoNaoEncontrado);

            // Modifica objeto
            curso.Atualizar(cursoVM.Nome, cursoVM.Sigla, cursoVM.Descricao);

            // Validação objeto
            if (!curso.EstaValido())
                return ResponseFactory<Curso>.BadRequest(curso.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Validação BD
            bool professorDoCurso = await _uow.Cursos.ProfessorDoCurso(cursoId, pessoaId);
            if (!professorDoCurso)
                return ResponseFactory<Curso>.Forbidden(_cursoResource.CursoNaoPermitido);

            // TODO: Essa validação é necessária? 
            if (await CursoExisteNaInstituicao(curso))
                return ResponseFactory<Curso>.BadRequest(_cursoResource.CursoJaExiste);

            // Salva no BD
            _uow.Cursos.Update(curso);

            if (!await _uow.CompleteAsync()) return ResponseFactory<Curso>.InternalServerError(_sharedResource.FalhaAtualizar);

            return ResponseFactory<Curso>.NoContent(_sharedResource.AtualizadoSucesso);
        }

        public async Task<ResponseAPI<Curso>> AlternarAtivacaoCurso(Guid cursoId, Guid pessoaId)
        {
            // Validação BD
            Curso curso = await _uow.Cursos.CursoCompleto(cursoId);

            if (curso == null)
                return ResponseFactory<Curso>.NotFound(_cursoResource.CursoNaoEncontrado);

            // TODO: Professor admin da Instituicao ou professor do curso?
            bool professorDoCurso = await _uow.Cursos.ProfessorDoCurso(cursoId, pessoaId);
            if (!professorDoCurso)
                return ResponseFactory<Curso>.Forbidden(_cursoResource.CursoNaoPermitido);

            if (curso.Ativo) curso.Desativar();
            else curso.Ativar();

            // Salva no BD
            _uow.Cursos.Update(curso);

            if (!await _uow.CompleteAsync()) return ResponseFactory<Curso>.InternalServerError(_sharedResource.FalhaDeletar);

            return ResponseFactory<Curso>.NoContent(_sharedResource.DeletadoSucesso);
        }

        private async Task<bool> CursoExisteNaInstituicao(Curso curso)
        {
            // Verifica se está tentando atualizar para um curso que já existe

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
