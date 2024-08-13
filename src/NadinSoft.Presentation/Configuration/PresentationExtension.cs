using Microsoft.Extensions.DependencyInjection;

namespace NadinSoft.Presentation.Configuration
{
    public static class PresentationExtension
    {
        public static IServiceCollection AddHttpConetxt(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
