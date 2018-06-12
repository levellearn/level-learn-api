using LevelLearn.Domain.Pessoas;
using Microsoft.AspNetCore.Identity;

namespace LevelLearn.Web.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
