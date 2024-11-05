using Core.Tokens.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Service.Abstracts;
using ToDoList.Service.Concretes;
using ToDoList.Service.Profiles;
using ToDoList.Service.Rules.Categories;
using ToDoList.Service.Rules.ToDos;
using ToDoList.Service.Rules.Users;

namespace ToDoList.Service
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(ToDoMappingProfile));
            //services.AddAutoMapper(typeof(CategoryMappingProfile));


            services.AddScoped<ToDosBusinessRules>();
            services.AddScoped<UserBusinessRules>();
            services.AddScoped<CategoryBusinessRules>();

            services.AddScoped<IToDoService, ToDoService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<DecoderService>();

            return services;
        }
    }
}
