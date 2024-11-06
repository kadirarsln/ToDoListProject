using AutoMapper;
using Core.Responses;
using Core.Tokens.Services;
using ToDoList.DataAcces.Abstracts;
using ToDoList.Models.Dtos.ToDos.Requests;
using ToDoList.Models.Dtos.ToDos.Responses;
using ToDoList.Models.Entities;
using ToDoList.Service.Abstracts;
using ToDoList.Service.Rules.ToDos;

namespace ToDoList.Service.Concretes
{
    public class ToDoService(IToDoRepository _toDoRepository, IMapper _mapper, ToDosBusinessRules businessRules) : IToDoService
    {
        public async Task<ReturnModel<ToDoResponseDto>> AddAsync(CreateToDoRequest create, string userId)
        {
            businessRules.ToDoTitleCheck(create.Title);
            businessRules.ToDoPastDateCheck(create.StartDate);
            businessRules.ToDoDatesCheck(create.StartDate, create.EndDate);
            businessRules.ToDoCategoryCheckAsync(create.CategoryId);

            ToDo createdToDo = _mapper.Map<ToDo>(create);
            createdToDo.Id = Guid.NewGuid();
            createdToDo.UserId = userId;

            await _toDoRepository.AddAsync(createdToDo);

            ToDoResponseDto responseDto = _mapper.Map<ToDoResponseDto>(createdToDo);

            return new ReturnModel<ToDoResponseDto>
            {
                Data = responseDto,
                Message = "ToDo Added.",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<List<ToDoResponseDto>>> GetAllAsync()
        {
            List<ToDo> todos = await _toDoRepository.GetAllAsync();
            List<ToDoResponseDto> responseDtos = _mapper.Map<List<ToDoResponseDto>>(todos);

            return new ReturnModel<List<ToDoResponseDto>>
            {
                Data = responseDtos,
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<List<ToDoResponseDto>>> GetAllByCategoryIdAsync(int id)
        {
            var todos = await _toDoRepository.GetAllAsync(x => x.CategoryId == id, false);
            var responses = _mapper.Map<List<ToDoResponseDto>>(todos);

            return new ReturnModel<List<ToDoResponseDto>>
            {
                Data = responses,
                Message = "ToDos Listed.",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<List<ToDoResponseDto>>> GetAllByUserIdAsync(string id)
        {
            var todos = await _toDoRepository.GetAllAsync(x => x.UserId == id, false);

            var responses = _mapper.Map<List<ToDoResponseDto>>(todos);

            return new ReturnModel<List<ToDoResponseDto>>
            {
                Data = responses,
                Message = string.Empty,
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<ToDoResponseDto>> GetByIdAsync(Guid id)
        {
            var todo = await _toDoRepository.GetByIdAsync(id);
            businessRules.ToDoIsNullCheck(todo, id);

            var responseDto = _mapper.Map<ToDoResponseDto>(todo);

            return new ReturnModel<ToDoResponseDto>
            {
                Data = responseDto,
                Message = "ToDo Found",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<ToDoResponseDto>> RemoveAsync(Guid id)
        {
            var todo = await _toDoRepository.GetByIdAsync(id);
            businessRules.ToDoIsNullCheck(todo, id);

            var deletedPost = await _toDoRepository.RemoveAsync(todo);
            var responseDto = _mapper.Map<ToDoResponseDto>(deletedPost);

            return new ReturnModel<ToDoResponseDto>
            {
                Data = responseDto,
                Message = "ToDo Deleted",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<ToDoResponseDto>> UpdateAsync(UpdateToDoRequest updateToDo)
        {
            var todo = await _toDoRepository.GetByIdAsync(updateToDo.Id);
            businessRules.ToDoIsNullCheck(todo, updateToDo.Id);

            businessRules.ToDoTitleCheck(updateToDo.Title);
            businessRules.ToDoPastDateCheck(updateToDo.StartDate);
            businessRules.ToDoDatesCheck(updateToDo.StartDate, updateToDo.EndDate);
            businessRules.ToDoCategoryCheckAsync(updateToDo.CategoryId);

            ToDo update = new ToDo
            {
                CategoryId = todo.CategoryId,
                Description = updateToDo.Description,
                Title = updateToDo.Title,
                Priority = updateToDo.Priority,
                Completed = updateToDo.Completed,
                StartDate = updateToDo.StartDate,
                EndDate = updateToDo.EndDate,
                UserId = todo.UserId,
                CreatedDate = todo.CreatedDate,
            };

            ToDo updatedToDo = await _toDoRepository.UpdateAsync(update);

            ToDoResponseDto responseDto = _mapper.Map<ToDoResponseDto>(updatedToDo);

            return new ReturnModel<ToDoResponseDto>
            {
                Data = responseDto,
                Message = "ToDo Updated",
                StatusCode = 200,
                Success = true
            };
        }
    }
}
