using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.DataAcces.Abstracts;
using ToDoList.DataAcces.Concretes;
using ToDoList.DataAcces.Contexts;

namespace ToDoList.DataAcces
{
    public static class DataAccessRepositoryDependencies
    {
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(
        opt => opt.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

            services.AddScoped<IToDoRepository, EfToDoRepository>();
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();

            return services;
        }
    }
}
