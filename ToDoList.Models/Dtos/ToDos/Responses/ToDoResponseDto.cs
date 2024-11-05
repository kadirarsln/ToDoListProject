using ToDoList.Models.Enums;

namespace ToDoList.Models.Dtos.ToDos.Responses;

public sealed record ToDoResponseDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public Priority Priority { get; set; }
    public bool Completed { get; set; }
    public DateTime CreatedDate { get; init; }
    public DateTime EndDate { get; init; }
    public DateTime StartDate { get; init; }
    public string UserName { get; init; }
    public string CategoryName { get; init; }
}
