using Core.Repositories;
using ToDoList.DataAcces.Abstracts;
using ToDoList.DataAcces.Contexts;
using ToDoList.Models.Entities;

namespace ToDoList.DataAcces.Concretes;

public class EfCategoryRepository : EfRepositoryBase<BaseDbContext, Category, int>, ICategoryRepository
{
    public EfCategoryRepository(BaseDbContext context) : base(context)
    {
    }
}
