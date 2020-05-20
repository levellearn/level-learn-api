using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Resource;
using LevelLearn.Resource.Institucional;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel;
using LevelLearn.ViewModel.Institucional.Curso;
using LevelLearn.ViewModel.Institucional.Instituicao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Institucional
{
    public class CursoService : ServiceBase<Curso>, ICursoService
    {
        #region Ctor

        private readonly IUnitOfWork _uow;
        private readonly ISharedResource _sharedResource;
        private readonly CursoResource _resource;

        public CursoService(IUnitOfWork uow, ISharedResource sharedResource)
            : base(uow.Cursos)
        {
            _uow = uow;
            _sharedResource = sharedResource;
            _resource = new CursoResource();
        }

        #endregion

        public async Task<ResponseAPI<IEnumerable<Curso>>> ObterCursosProfessor(Guid instituicaoId, Guid pessoaId, PaginationFilterVM filterVM)
        {
            string searchFilter = filterVM.SearchFilter;
            int pageNumber = filterVM.PageNumber;
            int pageSize = filterVM.PageSize;

            var cursos = await _uow.Cursos.CursosInstituicaoProfessor(instituicaoId, pessoaId, searchFilter, pageNumber, pageSize);

            var total = await _uow.Cursos.TotalCursosInstituicaoProfessor(instituicaoId, pessoaId, searchFilter);

            return ResponseFactory<IEnumerable<Curso>>.Ok(cursos, total);
        }

        public async Task<ResponseAPI<Curso>> ObterCurso(Guid cursoId, Guid pessoaId)
        {
            Curso curso = await _uow.Cursos.CursoCompleto(cursoId);

            if (curso == null)
                return ResponseFactory<Curso>.NotFound(_resource.CursoNaoEncontrado);

            return ResponseFactory<Curso>.Ok(curso);
        }

        public async Task<ResponseAPI<Curso>> CadastrarCurso(CadastrarCursoVM cursoVM, Guid pessoaId)
        {
            // TODO: Somente pode criar cursos um professor Admin?
            //var isProfessorAdmin = await _uow.Instituicoes.IsProfessorAdmin(cursoVM.InstituicaoId, new Guid(pessoaId));
            //if (!isProfessorAdmin)
            //   return ResponseFactory<Curso>.Forbidden(_sharedLocalizer.InstituicaoNaoPermitida);

            var curso = new Curso(cursoVM.Nome, cursoVM.Sigla, cursoVM.Descricao, cursoVM.InstituicaoId);

            // Validação objeto
            if (!curso.EstaValido())
                return ResponseFactory<Curso>.BadRequest(curso.DadosInvalidos(), _sharedResource.DadosInvalidos);

            var pessoaCurso = new PessoaCurso(TiposPessoa.Professor, pessoaId, curso.Id);
            curso.AtribuirPessoa(pessoaCurso);

            // Validação BD
            if (await CursoExisteNaInstituicao(curso))
                return ResponseFactory<Curso>.BadRequest(_resource.CursoJaExiste);

            // Salva no BD
            await _uow.Cursos.AddAsync(curso);

            if (!await _uow.CompleteAsync()) return ResponseFactory<Curso>.InternalServerError(_sharedResource.FalhaCadastrar);

            return ResponseFactory<Curso>.Created(curso, _sharedResource.CadastradoSucesso);
        }

        public async Task<ResponseAPI<Curso>> EditarCurso(Guid cursoId, EditarCursoVM cursoVM, Guid pessoaId)
        {
            Curso cursoExistente = await _uow.Cursos.GetAsync(cursoId);

            if (cursoExistente == null)
                return ResponseFactory<Curso>.NotFound(_resource.CursoNaoEncontrado);

            // Modifica objeto
            cursoExistente.Atualizar(cursoVM.Nome, cursoVM.Sigla, cursoVM.Descricao);

            // Validação objeto
            if (!cursoExistente.EstaValido())
            {
                var dadosInvalidos = cursoExistente.DadosInvalidos();
                return ResponseFactory<Curso>.BadRequest(dadosInvalidos, _sharedResource.DadosInvalidos);
            }

            // Validação BD
            var isProfessorCurso = await _uow.Cursos.IsProfessorDoCurso(cursoId, pessoaId);
            if (!isProfessorCurso)
                return ResponseFactory<Curso>.Forbidden(_resource.CursoNaoPermitido);

            // TODO: Essa validação é necessária? 
            if (await CursoExisteNaInstituicao(cursoExistente))
                return ResponseFactory<Curso>.BadRequest(_resource.CursoJaExiste);

            // Salva no BD
            _uow.Cursos.Update(cursoExistente);

            if (!await _uow.CompleteAsync()) return ResponseFactory<Curso>.InternalServerError(_sharedResource.FalhaAtualizar);

            return ResponseFactory<Curso>.NoContent(_sharedResource.AtualizadoSucesso);
        }

        public async Task<ResponseAPI<Curso>> RemoverCurso(Guid cursoId, Guid pessoaId)
        {
            // TODO: Professor admin da Instituicao ou professor do curso?
            // Validação BD
            var isProfessorCurso = await _uow.Cursos.IsProfessorDoCurso(cursoId, pessoaId);
            if (!isProfessorCurso)
                return ResponseFactory<Curso>.Forbidden(_resource.CursoNaoPermitido);

            var cursoExistente = await _uow.Cursos.CursoCompleto(cursoId);

            if (cursoExistente == null)
                return ResponseFactory<Curso>.NotFound(_resource.CursoNaoEncontrado);

            // TODO: Remover ou desativar?
            //_uow.Cursos.Remove(cursoExistente);

            cursoExistente.Desativar();
            _uow.Cursos.Update(cursoExistente);

            if (!await _uow.CompleteAsync())
                return ResponseFactory<Curso>.InternalServerError(_sharedResource.FalhaDeletar);

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
