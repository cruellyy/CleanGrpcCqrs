using System.Net.Http.Headers;
using System.Text;
using Application.Models;
using Application.Models.Base;
using Grpc.Core;
using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json;
using LoginRequest = Application.Models.LoginRequest;

namespace Application.Services;

public class AccountService(IConfiguration configuration)
{
    public async Task<BaseResponse<LoginResponse?>?> Login(string login, string password)
    {
        var requestData = new LoginRequest
        {
            Login = login,
            Password = password
        };

        var gpsAddress = configuration["GpsAddress"];
        using var client = new HttpClient();
        var body = JsonConvert.SerializeObject(requestData);
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"{gpsAddress}/api/loginWeb", content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = JsonConvert.DeserializeObject<BaseResponse<LoginResponse?>?>(
                response.Content.ReadAsStringAsync().Result);
            return responseContent;
        }
        else
        {
            var responseContent = await response.Content.ReadFromJsonAsync<BaseResponse<LoginResponse?>?>();
            throw new RpcException(new Status(StatusCode.Unauthenticated,
                responseContent?.Error ?? "От сервера пришел пустой ответ."));
        }
    }

    public async Task<LoginResponse> RefreshToken(string? token)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, "Токен не указан."));
        }
        
        var gpsAddress = configuration["GpsAddress"];
        var request = new HttpRequestMessage(HttpMethod.Get, $"{gpsAddress}/api/refreshToken");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token[7..]);

        using var client = new HttpClient();
        
        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = JsonConvert.DeserializeObject<LoginResponse?>(
                response.Content.ReadAsStringAsync().Result);
            return responseContent!;
        }
        else
        {
            var responseContent = await response.Content.ReadFromJsonAsync<BaseResponse<LoginResponse?>?>();
            throw new RpcException(new Status(StatusCode.Unauthenticated,
                responseContent?.Error ?? "От сервера пришел пустой ответ."));
        }
    }
}