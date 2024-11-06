
using ToDoList.Models.Enums;

namespace ToDoList.Models.Dtos.ToDos.Requests;

public sealed record UpdateToDoRequest
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public Priority Priority { get; set; }
    public bool Completed { get; set; }
    public DateTime EndDate { get; init; }
    public DateTime StartDate { get; init; }
    public int CategoryId { get; init; }

}


