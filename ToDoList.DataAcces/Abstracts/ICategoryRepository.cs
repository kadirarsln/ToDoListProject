using Core.Repositories;
using ToDoList.Models.Entities;

namespace ToDoList.DataAcces.Abstracts;

public interface ICategoryRepository:IRepository<Category, int>
{

}
