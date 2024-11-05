using AutoMapper;

using ToDoList.Models.Dtos.ToDos.Requests;
using ToDoList.Models.Dtos.ToDos.Responses;
using ToDoList.Models.Entities;

namespace ToDoList.Service.Profiles;

public class ToDoMappingProfile : Profile
{
    protected ToDoMappingProfile()
    {
        CreateMap<CreateToDoRequest, ToDo>();
        CreateMap<UpdateToDoRequest, ToDo>();
        CreateMap<ToDo, ToDoResponseDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
            .ForMember(x => x.UserName, opt => opt.MapFrom(X => X.User.UserName));
    }
}
