
using Core.Responses;
using ToDoList.Models.Dtos.Categories.Requests;
using ToDoList.Models.Dtos.Categories.Responses;

namespace ToDoList.Service.Abstracts;

public interface ICategoryService
{
    Task<ReturnModel<List<CategoryResponseDto>>> GetAllAsync();
    Task<ReturnModel<CategoryResponseDto>> GetByIdAsync(int id);
    Task<ReturnModel<NoData>> AddAsync(CreateCategoryRequestDto createDto);
    Task<ReturnModel<NoData>> UpdateAsync(UpdateCategoryRequestDto updateDto);
    Task<ReturnModel<NoData>> RemoveAsync(int id);

}
