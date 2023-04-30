using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ordering.Api.Dto;

public class OrderCreationDto
{
    [Required]
    [NotNull]
    public Guid? Id { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "Can't create order without lines")]
    public List<OrderLineDto> Lines { get; set; }
}
