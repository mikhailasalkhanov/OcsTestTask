using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Ordering.Api.Dto;

public class OrderLineDto
{
    [Required]
    [NotNull]
    [JsonPropertyName("id")]
    public Guid? ProductId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Feild qty must be greater than 0")]
    public int Qty { get; set; }
}