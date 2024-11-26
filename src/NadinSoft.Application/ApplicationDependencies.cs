using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NadinSoft.Application.Abstractions.Data;
using NadinSoft.Application.Feature.Data;
using NadinSoft.Application.Features.Identity.Commands.Login;
using NadinSoft.Application.Features.Identity.Commands.Register;
using NadinSoft.Application.Features.Products.Commands.CreateProduct;
using NadinSoft.Application.Features.Products.Commands.DeleteProduct;
using NadinSoft.Application.Features.Products.Commands.UpdateProduct;
using System.Reflection;

namespace NadinSoft.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateProductCommand>,CreateProductCommandValidator>();
            services.AddScoped<IValidator<UpdateProductCommand>,UpdateProductCommandValidator>();
            services.AddScoped<IValidator<LoginCommand>,LoginCommandValidator>();
            services.AddScoped<IValidator<RegisterCommand>, RegsiterCommandValidator>();
            services.AddScoped<IValidator<DeleteProductCommand>, DeleteProductCommandValidator>();
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
