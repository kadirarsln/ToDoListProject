using Core.Exceptions;
using ToDoList.Models.Entities;

namespace ToDoList.Service.Rules.Categories
{
    public class CategoryBusinessRules
    {
        public void CategoryIsNullCheck(Category category)
        {
            if (category is null)
            {
                throw new NotFoundException("İlgili Kategori Bulunamadı");
            }
        }
    }
}
