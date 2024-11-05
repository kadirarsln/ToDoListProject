
using ToDoList.Models.Enums;

namespace ToDoList.Models.Dtos.ToDos.Requests;

public sealed record UpdateToDoRequest(Guid Id,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    Priority Priority,
    bool Completed,
    int CategoryId);

