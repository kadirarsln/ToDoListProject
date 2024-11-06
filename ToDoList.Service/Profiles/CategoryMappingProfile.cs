using AutoMapper;
using ToDoList.Models.Dtos.Categories.Requests;
using ToDoList.Models.Dtos.Categories.Responses;
using ToDoList.Models.Entities;

namespace ToDoList.Service.Profiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CreateCategoryRequestDto, ToDo>();
        CreateMap<UpdateCategoryRequestDto, ToDo>();
        CreateMap<ToDo, CategoryResponseDto>();
    }
}
