using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.ViewModel.Institucional.Curso;
using LevelLearn.ViewModel.Institucional.Instituicao;

namespace LevelLearn.ViewModel.AutoMapper
{
    /// <summary>
    /// Institucional VM To Domain
    /// </summary>
    public class InstitucionalVMToDomain : Profile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public InstitucionalVMToDomain()
        {
            InstituicaoMap();
            CursoMap();
            TurmaoMap();
        }

        private void InstituicaoMap()
        {
            CreateMap<CadastrarInstituicaoVM, Instituicao>()
                .ConstructUsing(c =>
                    new Instituicao(c.Nome, c.Sigla, c.Descricao, c.Cnpj, c.OrganizacaoAcademica, c.Rede,
                        c.CategoriaAdministrativa, c.NivelEnsino, c.Cep, c.Municipio, c.UF)
                );            
        }

        private void CursoMap()
        {
            CreateMap<CadastrarCursoVM, Curso>()
                .ConstructUsing(c =>
                    new Curso(c.Nome, c.Sigla, c.Descricao, c.InstituicaoId)
                );
        }

        private void TurmaoMap()
        {
            CreateMap<CadastrarTurmaVM, Turma>()
                .ConstructUsing(c =>
                    new Turma(c.Nome, c.Descricao, c.NomeDisciplina, c.CursoId, c.ProfessorId)
                );
        }



    }
}
