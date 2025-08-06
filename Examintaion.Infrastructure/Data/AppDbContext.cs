using Examination.Domain.Models;
using Examination.Infrastructure.Configurations.Auth;
using Examintaion.Infrastructure.Helpers.UserHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Common;

namespace Examination.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<string>, string>
    {
        private readonly IUserHelper userHelper;

        public DbSet<ExamQuestionsAnswer> ExamQuestionsAnswers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<SubjectExamConfiguration> SubjectConfigurations { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options, IUserHelper userHelper) : base(options)
        {
            this.userHelper = userHelper;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            var now = DateTime.UtcNow;
            foreach (var entity in ChangeTracker.Entries<BaseEntity>())
            {
                if (acceptAllChangesOnSuccess)
                {
                    switch (entity.State)
                    {

                        case EntityState.Added:
                            entity.Entity.CreatedAt = now;
                            break;

                        case EntityState.Modified:
                            entity.Entity.UpdatedAt = now;
                            break;

                        case EntityState.Deleted:
                            entity.Entity.DeletedAt = now;
                            break;
                    }

                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //SetGlobalFilters(modelBuilder);
            modelBuilder.Entity<Subject>().HasQueryFilter(s => s.DeletedAt == null);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleSeeding).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        private void SetGlobalFilters(ModelBuilder modelBuilder)
        {
        }
    }

    // Ensure ApplicationUser is public to match the accessibility of AppDbContext  
}
