using Microsoft.Extensions.DependencyInjection;
using Part_B.Service.Contract;

namespace Part_B.Service
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IGlossaryService, GlossaryService>();
            return services;
        }
    }
}