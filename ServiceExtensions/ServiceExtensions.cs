using University.Interfaces;

namespace University.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {
            services.AddScoped<IUniversityService, UniversityService>();
            return services;
        }
    }
}
