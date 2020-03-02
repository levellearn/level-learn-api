//using LevelLearn.Domain.Enum;
//using LevelLearn.Domain.Institucional;
//using LevelLearn.Domain.Pessoas;
//using LevelLearn.Repository.Base;
//using LevelLearn.Repository.Interfaces.Institucional;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;

//namespace LevelLearn.Infra.EFCore.Repositories.Institucional
//{
//    public class CursoRepository : CrudRepository<Curso>, ICursoRepository
//    {
//        public CursoRepository(DbContext context)
//            : base(context)
//        { }

//        public List<Curso> CursosInstituicaoProfessor(int professorId)
//        {
//            return _context.Set<PessoaInstituicao>()
//                            .Where(p => p.PessoaId == professorId && (p.Perfil == PerfilInstituicaoEnum.Admin || p.Perfil == PerfilInstituicaoEnum.Professor))
//                            .Select(p => p.Instituicao)
//                            .SelectMany(p => p.Cursos)
//                            .Include(p => p.Pessoas)
//                            .Include(p => p.Instituicao)
//                            .OrderBy(p => p.Nome)
//                            .ToList();
//        }

//        public List<Curso> CursosProfessor(int professorId)
//        {
//            return _context.Set<PessoaCurso>()
//                            .Where(p => p.PessoaId == professorId && p.Perfil == TipoPessoaEnum.Professor)
//                            .Select(p => p.Curso)
//                            .OrderBy(p => p.Nome)
//                            .ToList();
//        }

//        public bool IsProfessorDoCurso(int cursoId, int pessoaId)
//        {
//            return _context.Set<Curso>()
//                            .Where(p => p.CursoId == cursoId)
//                            .SelectMany(p => p.Pessoas)
//                            .Where(p => p.Perfil == TipoPessoaEnum.Professor && p.PessoaId == pessoaId)
//                            .Count() > 0;
//        }
//    }
//}
