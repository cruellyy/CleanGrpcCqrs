using Newtonsoft.Json;

namespace Application.Models.Base;

public class LoginResponse
{
    [JsonProperty("access_token")]
    public string? AccessToken { get; set; }
    
    [JsonProperty("access_token_refresh")]
    public string? RefreshToken { get; set; }
}