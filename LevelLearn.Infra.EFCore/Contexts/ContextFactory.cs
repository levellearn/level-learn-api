using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace LevelLearn.Infra.EFCore.Contexts
{
    class ContextFactory : IDesignTimeDbContextFactory<LevelLearnContext>
    {
        private const string DB_CONNECTION = @"Data Source=.\SQLEXPRESS; Initial Catalog=LevelLearn;Integrated Security=True;user id=gamification; password=Gamificando;";

        public LevelLearnContext CreateDbContext(string[] args)
        {
            var construtor = new DbContextOptionsBuilder<LevelLearnContext>();
            construtor.UseSqlServer(DB_CONNECTION);
            return new LevelLearnContext(construtor.Options);
        }
    }
}
