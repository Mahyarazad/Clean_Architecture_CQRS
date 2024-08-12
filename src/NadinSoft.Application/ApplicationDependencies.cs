using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NadinSoft.Application.Features.Identity.Commands.Login;
using NadinSoft.Application.Features.Identity.Commands.Register;
using NadinSoft.Application.Features.Products.Commands.CreateProduct;
using System.Reflection;

namespace NadinSoft.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateProductCommand>,CreateProductCommandValidator>();
            services.AddScoped<IValidator<LoginCommand>,LoginCommandValidator>();
            services.AddScoped<IValidator<RegisterCommand>, RegsiterCommandValidator>();
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
