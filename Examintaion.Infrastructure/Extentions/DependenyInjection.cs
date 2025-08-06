using Examination.Domain.Common;
using Examination.Domain.Interfaces.Repostoreis;
using Examination.Domain.Interfaces.Services;
using Examination.Domain.Models;
using Examination.Infrastructure.Data;
using Examination.Infrastructure.Repostories;
using Examintaion.Infrastructure.Data;
using Examintaion.Infrastructure.Helpers.UserHelpers;
using Examintaion.Infrastructure.RabbitMQMessageHandlers;
using Examintaion.Infrastructure.Repostories;
using Examintaion.Infrastructure.SingalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Interfaces.Repostoreis;

namespace Template.Infrastructure.Extentions
{
    public static class DependenyInjection
    {
        public static IServiceCollection InfrastructureInjector(this IServiceCollection services, ConfigurationManager configuration)
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

            services.AddSingleton<MongoDBContext>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sql")));


            services.Configure<MongoDBSettings>(x => configuration.GetSection("mongo"));
            services.AddScoped<INotoficationRepo, NotificationRepo>();

            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<IQuestionRepo, QuestionsRepo>();
            services.AddScoped<ExamRepo>();
            services.AddScoped<IMessagePublisher, RabbitMqPublisher>();
            services.AddScoped<IUserHelper, UserHelpers>();


            services.AddScoped<INotificationService, NotificationService>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
