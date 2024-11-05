using Core.Tokens.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoList.Models.Dtos.ToDos.Requests;
using ToDoList.Service.Abstracts;

namespace ToDoList.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDosController(IToDoService _toDoService, DecoderService decoderService) : ControllerBase
{

    [HttpPost("add")]
    public async Task<IActionResult> AddAsync([FromBody] CreateToDoRequest dto)
    {
        var userId = decoderService.GetUserId();
        var result = await _toDoService.AddAsync(dto, userId);

        return Ok(result);
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _toDoService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("category")]
    public async Task<IActionResult> GetAllByCategoryIdAsync([FromQuery] int categoryId)
    {
        var result = await _toDoService.GetAllByCategoryIdAsync(categoryId);
        return Ok(result);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetAllByUserIdAsync([FromQuery] string userId)
    {
        var result = await _toDoService.GetAllByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var result = await _toDoService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> RemoveAsync([FromQuery] Guid id)
    {
        var result = await _toDoService.RemoveAsync(id);
        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateToDoRequest dto)
    {
        var result = await _toDoService.UpdateAsync(dto);
        return Ok(result);
    }
}


