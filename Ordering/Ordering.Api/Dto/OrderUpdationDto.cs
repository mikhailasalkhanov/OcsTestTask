using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ordering.Domain;

namespace Ordering.Api.Dto;

public class OrderUpdationDto
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [Required]
    [EnumDataType(typeof(OrderStatus))]
    public string Status { get; set; }

    [Required] [MinLength(1)] 
    public List<OrderLineDto> Lines { get; set; }
}