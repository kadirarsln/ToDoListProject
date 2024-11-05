//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using ToDoList.Models.Dtos.Users.Requests;
//using ToDoList.Service.Abstracts;

//namespace ToDoList.WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsersController(IUserService _userService) : ControllerBase
//    {

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
//        {
//            var result = await _userService.RegisterAsync(dto);
//            return Ok(result);
//        }

//        [HttpGet("getbyemail")]
//        public async Task<IActionResult> GetByEmail([FromQuery] string email)
//        {
//            var result = await _userService.GetByEmailAsync(email);
//            return Ok(result);
//        }
//    }
//}
