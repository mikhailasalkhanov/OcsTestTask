
namespace Ordering.Api.Dto;

public class OrderResponseDto
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public string Created { get; set; }
    public List<OrderLineDto> Lines { get; set; }
}