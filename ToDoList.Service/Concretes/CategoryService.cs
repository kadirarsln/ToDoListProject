using AutoMapper;
using Core.Responses;
using ToDoList.DataAcces.Abstracts;
using ToDoList.Models.Dtos.Categories.Requests;
using ToDoList.Models.Dtos.Categories.Responses;
using ToDoList.Models.Entities;
using ToDoList.Service.Abstracts;
using ToDoList.Service.Rules.Categories;

namespace ToDoList.Service.Concretes;

public sealed class CategoryService(ICategoryRepository _categoryRepository, IMapper _mapper, CategoryBusinessRules businessRules) : ICategoryService
{
    public async Task<ReturnModel<NoData>> AddAsync(CreateCategoryRequestDto createDto)
    {
        businessRules.CategoryNameCheck(createDto.Name);
        await businessRules.CategoryNameUniqueCheck(createDto.Name);

        Category category = _mapper.Map<Category>(createDto);
        await _categoryRepository.AddAsync(category);

        return new ReturnModel<NoData>
        {
            Success = true,
            Message = "Category Added.",
            StatusCode = 200
        };
    }

    public async Task<ReturnModel<List<CategoryResponseDto>>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var responses = _mapper.Map<List<CategoryResponseDto>>(categories);

        return new ReturnModel<List<CategoryResponseDto>>
        {
            Data = responses,
            Success = true,
            StatusCode = 200,
            Message = string.Empty
        };
    }

    public async Task<ReturnModel<CategoryResponseDto>> GetByIdAsync(int id)
    {
        Category category = await _categoryRepository.GetByIdAsync(id);
        businessRules.CategoryIsNullCheck(category);

        var responseDto = _mapper.Map<CategoryResponseDto>(category);

        return new ReturnModel<CategoryResponseDto>
        {
            Success = true,
            Data = responseDto,
            Message = "Category Found",
            StatusCode = 200
        };
    }

    public async Task<ReturnModel<NoData>> RemoveAsync(int id)
    {
        Category category = await _categoryRepository.GetByIdAsync(id);
        businessRules.CategoryIsNullCheck(category);
        return new ReturnModel<NoData>
        {
            Success = true,
            Message = "Category Deleted",
            StatusCode = 200
        };
    }

    public async Task<ReturnModel<NoData>> UpdateAsync(UpdateCategoryRequestDto updateCategory)
    {
        Category category = await _categoryRepository.GetByIdAsync(updateCategory.Id);
        businessRules.CategoryIsNullCheck(category);

        businessRules.CategoryNameCheck(updateCategory.Name);
        await businessRules.CategoryNameUniqueCheck(updateCategory.Name);

        category.Name = updateCategory.Name;
        await _categoryRepository.UpdateAsync(category);

        return new ReturnModel<NoData>
        {
            Success = true,
            Message = "Category Updated.",
            StatusCode = 200
        };


    }
}
