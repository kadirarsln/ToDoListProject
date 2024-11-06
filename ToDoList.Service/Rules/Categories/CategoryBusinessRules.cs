using Core.Exceptions;
using ToDoList.DataAcces.Abstracts;
using ToDoList.Models.Entities;
namespace ToDoList.Service.Rules.Categories;

public class CategoryBusinessRules(ICategoryRepository _categoryRepository)
{

    public void CategoryIsNullCheck(Category? category)
    {
        if (category == null)
        {
            throw new NotFoundException("Category not found.");
        }
    }

    public void CategoryNameCheck(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessException("Category name cannot be empty.");
        }
    }

    public async Task CategoryNameUniqueCheck(string name)
    {
        var categories = await _categoryRepository.GetAllAsync(x => x.Name == name);
        if (categories.Any())
        {
            throw new BusinessException("Category name must be unique.");
        }
    }
}


