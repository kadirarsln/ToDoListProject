namespace ToDoList.Models.Dtos.Categories.Requests;

public sealed record UpdateCategoryRequestDto
{
    public int Id { get; init; }
    public string Name { get; init; }
}




