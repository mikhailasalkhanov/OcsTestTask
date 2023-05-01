using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Ordering.Api.Validators;

namespace Ordering.Api.Dto;

public class OrderCreationDto
{
    [Required]
    [NotNull]
    [JsonPropertyName("id")]
    public Guid? Id { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "Can't create order without lines")]
    [UniqueProductIds]
    public List<OrderLineDto> Lines { get; set; }
}
