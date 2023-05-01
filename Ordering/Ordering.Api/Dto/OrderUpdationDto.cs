using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ordering.Api.Validators;
using Ordering.Domain.Models;

namespace Ordering.Api.Dto;

public class OrderUpdationDto
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [Required]
    [EnumDataType(typeof(OrderStatus), ErrorMessage = "Order status must be 'New', 'Pending', 'Paid', 'SentForDelivery', 'Delivered', 'Completed'")]
    public string Status { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "Can't to update order without lines")]
    [UniqueProductIds]
    public List<OrderLineDto> Lines { get; set; }
}