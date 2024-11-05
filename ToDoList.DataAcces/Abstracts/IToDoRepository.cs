using Core.Repositories;
using ToDoList.Models.Entities;

namespace ToDoList.DataAcces.Abstracts;

public interface IToDoRepository : IRepository<ToDo, Guid>
{
}
