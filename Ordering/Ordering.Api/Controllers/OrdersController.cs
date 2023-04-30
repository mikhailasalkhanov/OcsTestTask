using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ordering.Api.Dto;
using Ordering.Application.Exceptions;
using Ordering.Domain;
using Ordering.Domain.Interfaces;
using Ordering.Domain.Models;

namespace Ordering.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly IMapper _mapper;
    
    public OrdersController(IOrderService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] OrderCreationDto dto)
    {
        var created = await _service.CreateAsync(_mapper.Map<Order>(dto));
        if (created is null)
        {
            return StatusCode(403, new ErrorDto("Failed to create order"));
        }
        
        return Ok(_mapper.Map<OrderResponseDto>(created));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var order = await _service.GetByIdAsync(id);
        if (order is null)
        {
            return NotFound(new ErrorDto("Order is not found"));
        }
        
        return Ok(_mapper.Map<OrderResponseDto>(order));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]OrderUpdationDto dto)
    {
        try
        {
            dto.Id = id;
            var updated = await _service.UpdateAsync(_mapper.Map<Order>(dto));
            if (updated is null)
            {
                return NotFound(new ErrorDto("Order is not found"));
            }
            
            return Ok(_mapper.Map<OrderResponseDto>(updated));
        }
        catch (OrderException e)
        {
            return StatusCode(403, new ErrorDto(e.Message));
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            var deleted = await _service.DeleteAsync(id);
            if (deleted is null)
            {
                return NotFound(new ErrorDto("Order is not found"));
            }
            
            return Ok();
        }
        catch (OrderException e)
        {
            return StatusCode(403, new ErrorDto(e.Message));
        }
    }
}