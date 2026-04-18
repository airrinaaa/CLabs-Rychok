using ClassManager.Repositories;
using ClassManager.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace ClassManager.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddClassManagerDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IStorageContext, SQLiteStorageContext>();
            services.AddSingleton<ISubjectRepository, SubjectRepository>();
            services.AddSingleton<ILessonRepository, LessonRepository>();

            services.AddSingleton<ISubjectService, SubjectService>();
            services.AddTransient<ILessonService, LessonService>();

            return services;
        }
    }
}