using Newtonsoft.Json;

namespace Application.Models;

public class LoginRequest
{
    [JsonProperty("login_ad")]
    public string? Login { get; set; }
    
    [JsonProperty("password")]
    public string? Password { get; set; }
}