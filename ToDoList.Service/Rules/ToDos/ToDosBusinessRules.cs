using Core.Exceptions;
using ToDoList.DataAcces.Abstracts;
using ToDoList.DataAcces.Concretes;
using ToDoList.Models.Entities;

namespace ToDoList.Service.Rules.ToDos
{
    public class ToDosBusinessRules(IToDoRepository _toDoRepository, ICategoryRepository _categoryRepository)
    {
        public void ToDoIsNullCheck(ToDo toDo, Guid id)
        {

            if (toDo is null)
            {
                throw new NotFoundException($"ToDo with ID {id} not found.");
            }
        }
        public void ToDoTitleCheck(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new BusinessException("ToDo title cannot be empty.");
            }
        }

        public void ToDoDatesCheck(DateTime startDate, DateTime endDate)
        {
            if (endDate <= startDate)
            {
                throw new BusinessException("End date must be after the start date.");
            }
        }
        public void ToDoCompletionCheck(ToDo toDo)
        {
            if (toDo.Completed)
            {
                throw new BusinessException("A completed ToDo item cannot be updated.");
            }
        }
        public async Task ToDoCategoryCheckAsync(int categoryId)
        {
            bool categoryExists = await CheckCategoryExistsAsync(categoryId);
            if (!categoryExists)
            {
                throw new BusinessException("Invalid category ID.");
            }
        }
        private async Task<bool> CheckCategoryExistsAsync(int categoryId)
        {
            var categories = await _categoryRepository.GetAllAsync(c => c.Id == categoryId, false);
            return categories.Any();
        }

        public void ToDoPastDateCheck(DateTime startDate)
        {
            if (startDate < DateTime.UtcNow)
            {
                throw new BusinessException("The start date cannot be in the past.");
            }
        }
    }
}
