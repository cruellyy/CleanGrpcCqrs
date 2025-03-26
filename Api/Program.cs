using Application;
using Application.MediatR.Interceptors;
using Infrastructure;
using NevaSms.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Подключаем все слои
services.AddInfrastructure(configuration);
services.AddApplication();

// Регистрация сервисов gRPC с интерцептором
builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<ErrorHandlingInterceptor>();
    options.Interceptors.Add<TokenValidationInterceptor>();
});

var app = builder.Build();

// Configure the gRPC services.
app.MapGrpcService<MessageSampleServiceGrpc>();
app.MapGrpcService<AccountServiceGrpc>();

app.Run();