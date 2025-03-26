using Newtonsoft.Json;

namespace Application.Models.Base;

public class BaseResponse<T>
{
    [JsonProperty("response")]
    public ResponseModel<T>? Response { get; set; }
    
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}

public class ResponseModel<T>
{
    [JsonProperty("response")]
    public T? Response { get; set; }
}