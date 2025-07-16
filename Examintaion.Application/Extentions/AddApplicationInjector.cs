using Examination.Application.Interfaces.ExamService;
using Examination.Application.Interfaces.QuestionService;
using Examination.Application.Interfaces.SubjectService;
using Examination.Application.Services.ExamService;
using Examination.Application.Services.QuestionService;
using Examination.Application.Services.SubjectService;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Interfaces.Auth;
using Template.Application.Interfaces.File;
using Template.Application.Services.Auth;
using Template.Application.Services.FileHanlder;
using Template.Application.Shared.Mapper;

namespace Template.Application.Extentions
{
    public static class DepndencyInjection
    {
        public static IServiceCollection AddApplicationInjector(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddScoped<IAuthServices, AuthService>();
            services.AddScoped<IFileHandlerService, FileHandlerService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IExamService, ExamService>();

            return services;
        }
    }
}
