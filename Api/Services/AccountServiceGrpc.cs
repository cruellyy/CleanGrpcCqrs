using MediatR;
using Application.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace NevaSms.Services;

/// <summary>
/// Сервис для работы с пользователями из внешней системы
/// </summary>
/// <param name="accountService"></param>
public class AccountServiceGrpc(AccountService accountService) : Account.AccountBase
{
    /// <summary>
    /// Авторизация пользователя по логину и паролю
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        var result = await accountService.Login(request.Login, request.Password);

        return new LoginResponse
        {
            Token = result?.Response?.Response?.AccessToken ?? string.Empty,
            RefreshToken = result?.Response?.Response?.RefreshToken ?? string.Empty
        };
    }

    public override async Task<LoginResponse> RefreshToken(Empty request, ServerCallContext context)
    {
        context.RequestHeaders.GetValue("Authorization");
        
        var result = await accountService.RefreshToken(context.RequestHeaders.GetValue("Authorization"));

        return new LoginResponse
        {
            Token = result.AccessToken ?? string.Empty,
            RefreshToken = result.RefreshToken ?? string.Empty
        };
    }
}