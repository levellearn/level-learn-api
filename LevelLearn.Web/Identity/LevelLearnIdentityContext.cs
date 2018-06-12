using LevelLearn.Domain.Pessoas;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Web.Identity
{
    public class LevelLearnIdentityContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public LevelLearnIdentityContext(DbContextOptions<LevelLearnIdentityContext> options)
            : base(options)
        { }

        public LevelLearnIdentityContext()
            : base()
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Pessoa>().ToTable("Pessoas");
            builder.Entity<ApplicationUser>().HasOne(p => p.Pessoa).WithOne();
        }
    }
}
