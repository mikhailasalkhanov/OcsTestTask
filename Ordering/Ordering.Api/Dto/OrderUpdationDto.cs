using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ordering.Domain.Models;

namespace Ordering.Api.Dto;

public class OrderUpdationDto
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Order status must be 'New', 'Pending', 'Paid', 'SentForDelivery', 'Delivered', 'Completed'")]
    [EnumDataType(typeof(OrderStatus))]
    public string Status { get; set; }

    [Required(ErrorMessage = "Can't to update order without lines")]
    [MinLength(1, ErrorMessage = "Qty must be greater than 0")]
    public List<OrderLineDto> Lines { get; set; }
}