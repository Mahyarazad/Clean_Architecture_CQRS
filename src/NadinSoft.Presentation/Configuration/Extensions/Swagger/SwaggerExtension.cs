using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NadinSoft.Presentation.Configuration.Extensions.Swagger
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen( options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "NadinSoft", Version = "v1"});
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NadinSoft v1"));
            return app;
        }
    }
}
