using Examination.Domain.Interfaces.Repostoreis;
using Examination.Domain.Models;
using Examination.Infrastructure.Data;
using Examination.Infrastructure.Repostories;
using Examintaion.Infrastructure.Repostories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Interfaces.Repostoreis;

namespace Template.Infrastructure.Extentions
{
    public static class DependenyInjection
    {
        public static IServiceCollection InfrastructureInjector(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole>()
            .AddUserManager<UserManager<AppUser>>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<AppDbContext>()
            ;

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sql")));

            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<IQuestionRepo, QuestionsRepo>();

            return services;
        }
    }
}
