using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MTG_OnlineTraining.Models;

namespace MTG_OnlineTraining.Data
{
    public class ApplicationDbConntext : IdentityDbContext
    {
        public ApplicationDbConntext(DbContextOptions<ApplicationDbConntext> options) : base(options)
        {
        }
        public DbSet<StudentPrograms> StudentPrograms { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<AdminProgram> AdminProgram { get; set; }

    }
}
