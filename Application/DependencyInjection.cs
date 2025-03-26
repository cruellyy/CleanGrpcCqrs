using System.Reflection;
using Application.Mappers;
using Application.MediatR.Behaviors;
using Application.MediatR.Interceptors;
using Application.MediatR.Validators;
using Application.Services;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        // Регистрируем AutoMapper
        services.AddAutoMapper(typeof(MappingProfile));
        
        // Регистрация MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        // Регистрация FluentValidation
        services.AddValidatorsFromAssemblyContaining<CreateMessageSampleCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<EditMessageSampleCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<DeleteMessageSampleCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<InfoMessageSampleCommandValidator>();
        
        // Регистрируем ValidatorBehavior, чтобы MediatR использовал валидацию в пайплайне
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        services.AddFluentValidation();
        services.AddScoped<AccountService>();
        
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        });

        // Регистрация интерцептора
        services.AddSingleton<ErrorHandlingInterceptor>();
    }
}