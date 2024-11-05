using Core.Exceptions;
using ToDoList.Models.Entities;

namespace ToDoList.Service.Rules.ToDos
{
    public class ToDosBusinessRules
    {
        public void ToDoIsNullCheck(ToDo todo)
        {
            
            if (todo is null)
            {
                throw new NotFoundException("İlgili ToDo Bulunamadı");
            }
        }
    }
}
