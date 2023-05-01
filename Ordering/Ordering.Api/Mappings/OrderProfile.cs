using AutoMapper;
using Ordering.Api.Dto;
using Ordering.Domain.Models;

namespace Ordering.Api.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderCreationDto, Order>();
        
        CreateMap<OrderLineDto, OrderLine>()
            .ForMember(dest => dest.Quantity, 
                opt => opt.MapFrom(src => src.Qty))
            .ReverseMap();
        
        CreateMap<OrderUpdationDto, Order>().AfterMap((dto, order) =>
        {
            foreach (var line in order.Lines!)
            {
                line.OrderId = order.Id;
            }
        });
        
        CreateMap<Order, OrderResponseDto>()
            .ForMember(dest => dest.Created, 
                opt => opt.MapFrom(src =>
                    src.CreatedAt.ToString("yyyy-MM-dd HH:mm.ss")));
    }
}