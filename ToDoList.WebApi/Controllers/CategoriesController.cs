using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.Dtos.Categories.Requests;
using ToDoList.Service.Abstracts;

namespace ToDoList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService _categoryService) : ControllerBase
    {
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddAsync([FromBody] CreateCategoryRequestDto dto)
        {
            var result = _categoryService.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromBody] UpdateCategoryRequestDto dto)
        {
            var result = _categoryService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAllAsync()
        {
            var result = _categoryService.GetAllAsync();
            return Ok(result);
        }


        [HttpGet("getbyid")]
        public IActionResult GetByIdAsync([FromQuery] int id)
        {
            var result = _categoryService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveAsync([FromQuery] int id)
        {
            var result = _categoryService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
