using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NadinSoft.Application.Features.Products.Commands.CreateProduct;
using System.Reflection;

namespace NadinSoft.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateProductCommand>,CreateProductCommandValidator>();
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
