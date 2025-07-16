using Examination.Domain.Models;
using Examination.Infrastructure.Configurations.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Examination.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<string>, string>
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<SubjectExamConfiguration> SubjectConfigurations { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleSeeding).Assembly);
        }
    }

    // Ensure ApplicationUser is public to match the accessibility of AppDbContext  
}
