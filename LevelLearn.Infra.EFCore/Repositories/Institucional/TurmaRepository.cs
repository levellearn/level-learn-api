//using LevelLearn.Domain.Institucional;
//using LevelLearn.Repository.Base;
//using LevelLearn.Repository.Interfaces.Institucional;
//using Microsoft.EntityFrameworkCore;
//using System;

//namespace LevelLearn.Repositories.Institucional
//{
//    public class TurmaRepository : CrudRepository<Turma>, ITurmaRepository
//    {
//        public TurmaRepository(DbContext context)
//            : base(context)
//        { }

//        public override bool Update(Turma turma)
//        {
//            try
//            {
//                if (_context.Entry(turma).State == EntityState.Detached)
//                    _context.Set<Turma>().Attach(turma);
//                _context.Entry(turma).State = EntityState.Modified;
//                _context.Entry(turma).Property(p => p.ProfessorId).IsModified = false;
//                _context.SaveChanges();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }
//    }
//}
