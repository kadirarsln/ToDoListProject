
namespace ToDoList.Models.Dtos.Categories.Requests;

public sealed record CreateCategoryRequestDto
{
    public string Name { get; init; }
}
