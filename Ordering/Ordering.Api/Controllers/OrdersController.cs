using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ordering.Api.Dto;
using Ordering.Application.Interfaces;
using Ordering.Domain;

namespace Ordering.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
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
        var result = await _service.CreateAsync(_mapper.Map<Order>(dto));

        if (!result.IsSuccess)
        {
            return StatusCode(403, new Error(result.ErrorMessage!));
        }
        
        var responseDto = _mapper.Map<OrderResponseDto>(result.Value);
        return Ok(responseDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result.IsSuccess)
        {
            return Ok(_mapper.Map<OrderResponseDto>(result.Value));
        }

        return NotFound(new Error(result.ErrorMessage!));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]OrderUpdationDto dto)
    {
        dto.Id = id;
        var result = await _service.UpdateAsync(_mapper.Map<Order>(dto));
        if (result.IsSuccess)
        {
            var responseDto = _mapper.Map<OrderResponseDto>(result.Value);
            return Ok(responseDto);
        }

        if (result.Value is null)
        {
            return NotFound(new Error(result.ErrorMessage!));
        }
        
        return StatusCode(403, new Error(result.ErrorMessage!));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _service.DeleteAsync(id);
        if (result.IsSuccess)
        {
            return Ok();
        }

        if (result.Value is null)
        {
            return NotFound(new Error(result.ErrorMessage!));
        }

        return StatusCode(403, new Error(result.ErrorMessage!));
    }
}