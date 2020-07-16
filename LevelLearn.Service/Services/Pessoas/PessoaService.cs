using AutoMapper;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Resource;
using LevelLearn.Service.Interfaces.Pessoas;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Pessoas;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Pessoas
{
    public abstract class PessoaService : ServiceBase<Pessoa, Guid>, IPessoaService
    {
        protected readonly IUnitOfWork _uow;
        protected readonly ISharedResource _sharedResource;
        protected readonly UserManager<Usuario> _userManager;
        protected readonly PessoaResource _pessoaResource;
        protected readonly IMapper _mapper;

        public PessoaService(IUnitOfWork uow, ISharedResource sharedResource, UserManager<Usuario> userManager, IMapper mapper)
            : base(uow.Pessoas)
        {
            _uow = uow;
            _sharedResource = sharedResource;
            _userManager = userManager;
            _pessoaResource = PessoaResource.ObterInstancia();
            _mapper = mapper;
        }

        protected async Task<ResultadoService> AtualizarPatch<TEntity>(Pessoa pessoa, string usuarioId, JsonPatchDocument<TEntity> patch)
          where TEntity : PessoaAtualizaVM
        {
            if (pessoa == null) return ResultadoServiceFactory.NotFound(_sharedResource.NaoEncontrado);

            var pessoaVMToPatch = _mapper.Map<TEntity>(pessoa);
            patch.ApplyTo(pessoaVMToPatch);
            _mapper.Map(pessoaVMToPatch, pessoa);

            if (!pessoa.EstaValido())
                return ResultadoServiceFactory.BadRequest(pessoa.DadosInvalidos(), _sharedResource.DadosInvalidos);

            foreach (string op in patch.Operations.Select(p => p.path))
            {
                switch (op.Replace("/", string.Empty).ToLower())
                {
                    case "cpf":
                        if (await _uow.Pessoas.EntityExists(p => p.Cpf.Numero == pessoaVMToPatch.Cpf && p.Id != pessoa.Id))
                            return ResultadoServiceFactory.BadRequest(_pessoaResource.PessoaCPFJaExiste);
                        break;
                    case "nome":
                        Usuario usuario = await _userManager.FindByIdAsync(usuarioId);
                        var resultadoIdentity = await AtualizarUsuario(usuario, pessoa);
                        if (resultadoIdentity.Falhou)
                            return resultadoIdentity;
                        break;
                    default:
                        break;

                }
            }

            _uow.Pessoas.Update(pessoa);
            if (!await _uow.CommitAsync())
                return ResultadoServiceFactory.InternalServerError(_sharedResource.FalhaAtualizar);

            return ResultadoServiceFactory.NoContent(_sharedResource.AtualizadoSucesso);
        }

        private async Task<ResultadoService> AtualizarUsuario(Usuario usuario, Pessoa pessoa)
        {
            usuario.Nome = pessoa.Nome;

            if (!usuario.EstaValido())
                return ResultadoServiceFactory.BadRequest(usuario.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Atualizando USUÁRIO BD
            IdentityResult identityResult = await _userManager.UpdateAsync(usuario);

            if (!identityResult.Succeeded)
                return ResultadoServiceFactory<Usuario>.BadRequest(identityResult.GetErrorsResult(), _sharedResource.DadosInvalidos);

            return ResultadoServiceFactory.NoContent();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}
