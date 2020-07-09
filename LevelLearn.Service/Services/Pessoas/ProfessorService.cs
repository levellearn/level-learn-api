using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Resource;
using LevelLearn.Service.Interfaces.Pessoas;
using LevelLearn.Service.Response;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Pessoas
{
    public class ProfessorService : ServiceBase<Professor, Guid>, IProfessorService
    {
        private readonly IUnitOfWork _uow;
        private readonly ISharedResource _sharedResource;
        private readonly UserManager<Usuario> _userManager;
        private readonly PessoaResource _pessoaResource;

        public ProfessorService(IUnitOfWork uow, ISharedResource sharedResource, UserManager<Usuario> userManager)
            : base(uow.Professores)
        {
            _uow = uow;
            _sharedResource = sharedResource;
            _userManager = userManager;
            _pessoaResource = PessoaResource.ObterInstancia();
        }

        public async Task<ResultadoService<IEnumerable<Professor>>> ObterProfessorsPorInstituicao(Guid instituicaoId, FiltroPaginacao filtroPaginacao)
        {
            IEnumerable<Professor> professores = await _uow.Professores.ObterProfessoresPorInstituicao(instituicaoId, filtroPaginacao);
            int total = await _uow.Professores.TotalProfessoresPorInstituicao(instituicaoId, filtroPaginacao);

            return ResultadoServiceFactory<IEnumerable<Professor>>.Ok(professores, total);
        }

        public async Task<ResultadoService> Atualizar(string usuarioId, Professor professor)
        {
            if (!professor.EstaValido())
                return ResultadoServiceFactory.BadRequest(professor.DadosInvalidos(), _sharedResource.DadosInvalidos);

            Usuario usuario = await _userManager.FindByIdAsync(usuarioId);

            if (usuario.Nome != professor.Nome)
            {
                usuario.Nome = professor.Nome;

                if (!usuario.EstaValido())
                    return ResultadoServiceFactory.BadRequest(usuario.DadosInvalidos(), _sharedResource.DadosInvalidos);

                // Atualizando USUÁRIO BD
                IdentityResult identityResult = await _userManager.UpdateAsync(usuario);

                if (!identityResult.Succeeded)
                    return ResultadoServiceFactory<Usuario>.BadRequest(identityResult.GetErrorsResult(), _sharedResource.DadosInvalidos);
            }

            // Verifica se CPF já existe 
            // TODO: Mudar CPF validar
            //if (await _uow.Pessoas.EntityExists(i => i.Cpf.Numero == professor.Cpf.Numero))
            //    return ResultadoServiceFactory.BadRequest(_pessoaResource.PessoaCPFJaExiste);

            _uow.Professores.Update(professor);
            if (!await _uow.CommitAsync())
                return ResultadoServiceFactory.InternalServerError(_sharedResource.FalhaAtualizar);

            return ResultadoServiceFactory.NoContent(_sharedResource.AtualizadoSucesso);
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}
