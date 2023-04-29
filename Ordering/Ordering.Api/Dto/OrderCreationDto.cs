using System.ComponentModel.DataAnnotations;

namespace Ordering.Api.Dto;

public class OrderCreationDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [MinLength(1)]
    public List<OrderLineDto> Lines { get; set; }
}
