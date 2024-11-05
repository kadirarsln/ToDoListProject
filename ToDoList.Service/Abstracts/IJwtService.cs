using ToDoList.Models.Dtos.Tokens;
using ToDoList.Models.Entities;

namespace ToDoList.Service.Abstracts;

public interface IJwtService
{
    Task<TokenResponseDto> CreateTokenAsync(User user);
}
