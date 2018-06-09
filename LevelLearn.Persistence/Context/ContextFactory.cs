using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace LevelLearn.Persistence.Context
{
    class ContextFactory : IDesignTimeDbContextFactory<LevelLearnContext>
    {
        private const string CONNECTIONSTRING = @"Data Source=.\SQLEXPRESS; Initial Catalog=LevelLearn;Integrated Security=True;user id=gamification; password=Gamificando;";

        public LevelLearnContext CreateDbContext(string[] args)
        {
            var construtor = new DbContextOptionsBuilder<LevelLearnContext>();
            construtor.UseSqlServer(CONNECTIONSTRING);
            return new LevelLearnContext(construtor.Options);
        }
    }
}
