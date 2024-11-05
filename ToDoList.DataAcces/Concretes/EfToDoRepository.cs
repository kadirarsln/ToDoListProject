using Core.Repositories;
using ToDoList.DataAcces.Abstracts;
using ToDoList.DataAcces.Contexts;
using ToDoList.Models.Entities;

namespace ToDoList.DataAcces.Concretes;

public class EfToDoRepository : EfRepositoryBase<BaseDbContext, ToDo, Guid>, IToDoRepository
{
    public EfToDoRepository(BaseDbContext context) : base(context)
    {
    }
}
