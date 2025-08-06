using Examination.Domain.Common;
using Examintaion.Infrastructure.SingalR;
using Scalar.AspNetCore;
using Template.API.Exctentions;
using Template.API.Middlewares;
using Template.Application.Extentions;
using Template.Infrastructure.Extentions;
namespace Template.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<MongoDBSettings>(
                builder.Configuration.GetSection("")
                );

            // Add services that is related to API Layer to the container.
            builder.Services.AddAPIDependencies(builder.Configuration);
            builder.Services.InfrastructureInjector(builder.Configuration);
            builder.Services.AddApplicationInjector();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseSwagger();

            app.UseCors("CorsPolicy");

            app.MapScalarApiReference("/scalar/v1");
            app.MapOpenApi();
            /*
            using (var scope = app.Services.CreateScope())
            {

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Seed admin user
                var email = "admin@exam.com";

                if (userMgr.FindByEmailAsync(email).GetAwaiter().GetResult() == null)
                {
                    var user = new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "admin",
                        Email = email,
                        EmailConfirmed = true,
                        PhoneNumber = "01225610933",
                    };

                    ;

                    if (roleMgr.FindByNameAsync("admin").GetAwaiter().GetResult() == null)
                    {
                        roleMgr.CreateAsync(new IdentityRole("admin")).GetAwaiter().GetResult();
                    }

                    var result = userMgr.CreateAsync(user, "Admin123").GetAwaiter().GetResult();

                    if (result.Succeeded)
                    {
                        userMgr.AddToRoleAsync(user, "admin").GetAwaiter().GetResult();
                    }




                }






            }
            */
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHub<NotificationHub>("/hub/not");
            app.MapControllers();

            app.Run();
        }
    }
}
