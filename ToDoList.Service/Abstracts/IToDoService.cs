using Core.Responses;
using ToDoList.Models.Dtos.ToDos.Requests;
using ToDoList.Models.Dtos.ToDos.Responses;

namespace ToDoList.Service.Abstracts;

public interface IToDoService
{
    Task<ReturnModel<List<ToDoResponseDto>>> GetAllAsync();
    Task<ReturnModel<ToDoResponseDto>> GetByIdAsync(Guid id);
    Task<ReturnModel<ToDoResponseDto>> AddAsync(CreateToDoRequest create, string userId);
    Task<ReturnModel<ToDoResponseDto>> UpdateAsync(UpdateToDoRequest updatePost);
    Task<ReturnModel<ToDoResponseDto>> RemoveAsync(Guid id);
    Task<ReturnModel<List<ToDoResponseDto>>> GetAllByCategoryIdAsync(int id);
    Task<ReturnModel<List<ToDoResponseDto>>> GetAllByUserIdAsync(string id);
}
