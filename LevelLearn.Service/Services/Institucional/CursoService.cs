using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.Validators.Institucional;
using LevelLearn.Resource;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Curso;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Institucional
{
    public class CursoService : ServiceBase<Curso>, ICursoService
    {
        private readonly IUnitOfWork _uow;
        private readonly ISharedResource _sharedLocalizer;
        private readonly IValidatorApp<Curso> _validator;

        public CursoService(IUnitOfWork uow, ISharedResource sharedLocalizer)
            : base(uow.Cursos)
        {
            _uow = uow;
            _sharedLocalizer = sharedLocalizer;
            _validator = new CursoValidator(_sharedLocalizer);
        }
        public Task<ResponseAPI<Curso>> CadastrarCurso(CadastrarCursoVM instituicaoVM, string pessoaId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
