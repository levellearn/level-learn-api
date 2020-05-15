using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.Validators.Institucional;
using LevelLearn.Resource;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Curso;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Institucional
{
    public class CursoService : ServiceBase<Curso>, ICursoService
    {
        private readonly IUnitOfWork _uow;
        private readonly ISharedResource _sharedLocalizer;
        private readonly IValidador<Curso> _validator;

        public CursoService(IUnitOfWork uow, ISharedResource sharedLocalizer)
            : base(uow.Cursos)
        {
            _uow = uow;
            _sharedLocalizer = sharedLocalizer;
            _validator = new CursoValidator(_sharedLocalizer);
        }

        public async Task<ResponseAPI<Curso>> CadastrarCurso(CadastrarCursoVM cursoVM, Guid pessoaId)
        {
            // Validação BD
            if (await _uow.Cursos.EntityExists(i => i.NomePesquisa == cursoVM.Nome.GenerateSlug()))
                return ResponseFactory<Curso>.BadRequest(_sharedLocalizer.CursoJaExiste);

            // TODO: Somente pode criar cursos um professor Admin?
            //var isProfessorAdmin = await _uow.Instituicoes.IsProfessorAdmin(cursoVM.InstituicaoId, new Guid(pessoaId));

            //if (!isProfessorAdmin)
            //   return ResponseFactory<Curso>.Forbidden(_sharedLocalizer.InstituicaoNaoPermitida);

            var curso = new Curso(cursoVM.Nome, cursoVM.Sigla, cursoVM.Descricao, cursoVM.InstituicaoId);

            // Validação objeto
            _validator.Validar(curso);

            if (!curso.EstaValido())
                return ResponseFactory<Curso>.BadRequest(curso.DadosInvalidos(), _sharedLocalizer.DadosInvalidos);

            var pessoaCurso = new PessoaCurso(TiposPessoa.Professor, pessoaId, curso.Id);
            curso.AtribuirPessoa(pessoaCurso);            

            // Salva no BD
            await _uow.Cursos.AddAsync(curso);

            if (!await _uow.CompleteAsync()) return ResponseFactory<Curso>.InternalServerError(_sharedLocalizer.FalhaCadastrar);

            return ResponseFactory<Curso>.Created(curso, _sharedLocalizer.CadastradoSucesso);
        }

        public async Task<ResponseAPI<Curso>> ObterCurso(Guid cursoId, Guid pessoaId)
        {
            Curso curso = await _uow.Cursos.GetAsync(cursoId);

            if (curso == null)
                return ResponseFactory<Curso>.NotFound(_sharedLocalizer.CursoNaoEncontrado);

            return ResponseFactory<Curso>.Ok(curso);
        }



        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}
