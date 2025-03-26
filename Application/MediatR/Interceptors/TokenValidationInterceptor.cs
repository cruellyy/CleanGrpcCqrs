using System.Net.Http.Headers;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Application.MediatR.Interceptors;

public class TokenValidationInterceptor(IConfiguration configuration) : Interceptor
{
    private readonly string[] _noAuthMethods = ["/account.Account/Login"]; // Методы, которые не требуют токен
    
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
        ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var methodName = context.Method;

        // Если метод в списке без авторизации, пропускаем проверку
        if (_noAuthMethods.Contains(methodName))
        {
            return await continuation(request, context);
        }
        
        // Получаем токен из метаданных запроса
        var token = context.RequestHeaders.GetValue("Authorization");

        if (string.IsNullOrEmpty(token))
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, "Токен не указан."));
        }

        // Проверка токена на внешнем сервере
        var isTokenValid = await ValidateTokenAsync(token[7..]);

        if (!isTokenValid)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, "Невалидный токен."));
        }

        // Если токен валидный, продолжаем выполнение запроса
        return await continuation(request, context);
    }

    private async Task<bool> ValidateTokenAsync(string token)
    {
        var gpsAddress = configuration["GpsAddress"];
        var request = new HttpRequestMessage(HttpMethod.Get, $"{gpsAddress}/api/check-token");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        using var client = new HttpClient();
        var response = await client.SendAsync(request);

        return response.IsSuccessStatusCode;
    }
}