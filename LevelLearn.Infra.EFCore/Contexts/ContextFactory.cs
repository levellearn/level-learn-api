using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LevelLearn.Infra.EFCore.Contexts
{
    //public class ContextFactory : IDesignTimeDbContextFactory<LevelLearnContext>
    //{
    //    //private const string DB_CONNECTION = @"Data Source=.\SQLEXPRESS; Initial Catalog=LevelLearn;Integrated Security=True;user id=gamification; password=Gamificando;";
    //    private const string DB_CONNECTION = @"Server=(localdb)\\mssqllocaldb;Database=LevelLearn;Trusted_Connection=True;MultipleActiveResultSets=true";

    //    public LevelLearnContext CreateDbContext(string[] args)
    //    {
    //        var construtor = new DbContextOptionsBuilder<LevelLearnContext>();
    //        construtor.UseSqlServer(DB_CONNECTION);
    //        return new LevelLearnContext(construtor.Options);
    //    }
    //}
}
