using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ordering.Api.Dto;

public class OrderLineDto
{
    [Required]
    [JsonPropertyName("id")]
    public Guid ProductId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Qty { get; set; }
}