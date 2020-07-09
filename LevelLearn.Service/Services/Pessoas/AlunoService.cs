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
    public class AlunoService : ServiceBase<Aluno, Guid>, IAlunoService
    {
        private readonly IUnitOfWork _uow;
        private readonly ISharedResource _sharedResource;
        private readonly PessoaResource _pessoaResource;
        private readonly UserManager<Usuario> _userManager;

        public AlunoService(IUnitOfWork uow, ISharedResource sharedResource, UserManager<Usuario> userManager)
            : base(uow.Alunos)
        {
            _uow = uow;
            _sharedResource = sharedResource;
            _userManager = userManager;
            _pessoaResource = PessoaResource.ObterInstancia();
        }

        public async Task<ResultadoService<IEnumerable<Aluno>>> ObterAlunosPorInstituicao(Guid instituicaoId, FiltroPaginacao filtroPaginacao)
        {
            IEnumerable<Aluno> alunos = await _uow.Alunos.ObterAlunosPorInstituicao(instituicaoId, filtroPaginacao);
            int total = await _uow.Alunos.TotalAlunosPorInstituicao(instituicaoId, filtroPaginacao);

            return ResultadoServiceFactory<IEnumerable<Aluno>>.Ok(alunos, total);
        }

        public async Task<ResultadoService<IEnumerable<Aluno>>> ObterAlunosPorCurso(Guid cursoId, FiltroPaginacao filtroPaginacao)
        {
            IEnumerable<Aluno> alunos = await _uow.Alunos.ObterAlunosPorCurso(cursoId, filtroPaginacao);
            int total = await _uow.Alunos.TotalAlunosPorCurso(cursoId, filtroPaginacao);

            return ResultadoServiceFactory<IEnumerable<Aluno>>.Ok(alunos, total);
        }

        public async Task<ResultadoService> Atualizar(string usuarioId, Aluno aluno)
        {
            if (!aluno.EstaValido())
                return ResultadoServiceFactory.BadRequest(aluno.DadosInvalidos(), _sharedResource.DadosInvalidos);

            Usuario usuario = await _userManager.FindByIdAsync(usuarioId);

            if (usuario.Nome != aluno.Nome)
            {
                usuario.Nome = aluno.Nome;

                if (!usuario.EstaValido())
                    return ResultadoServiceFactory.BadRequest(usuario.DadosInvalidos(), _sharedResource.DadosInvalidos);

                // Atualizando USUÁRIO BD
                IdentityResult identityResult = await _userManager.UpdateAsync(usuario);

                if (!identityResult.Succeeded)
                    return ResultadoServiceFactory<Usuario>.BadRequest(identityResult.GetErrorsResult(), _sharedResource.DadosInvalidos);
            }

            // Validações BD
            // TODO: Mudar CPF validar e mudar nome pesquisa
            //if (!string.IsNullOrWhiteSpace(aluno.Cpf.Numero))
            //{
            //    if (await _uow.Pessoas.EntityExists(i => i.Cpf.Numero == aluno.Cpf.Numero))
            //        return ResultadoServiceFactory.BadRequest(_pessoaResource.PessoaCPFJaExiste);
            //}

            _uow.Alunos.Update(aluno);
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
