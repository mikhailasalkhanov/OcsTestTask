using System.Text.Json.Serialization;

namespace Ordering.Api.Dto;

public class ErrorDto
{
    [JsonPropertyName("error")]
    public string? Message { get; set; }

    public ErrorDto(string message)
    {
        Message = message;
    }
}