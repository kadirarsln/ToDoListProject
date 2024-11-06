using AutoMapper;
using Moq;
using ToDoList.DataAcces.Abstracts;
using ToDoList.Models.Dtos.Categories.Requests;
using ToDoList.Models.Dtos.Categories.Responses;
using ToDoList.Models.Entities;
using ToDoList.Service.Concretes;
using ToDoList.Service.Rules.Categories;

namespace Service.Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<CategoryBusinessRules> _businessRulesMock;
        private CategoryService _categoryService;

        [SetUp]
        public void SetUp()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<IMapper>();
            _businessRulesMock = new Mock<CategoryBusinessRules>(/* gerekli bağımlılıklar */);

            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object, _businessRulesMock.Object);
        }

        [Test]
        public async Task AddAsync_ShouldReturnSuccess_WhenCategoryIsAdded()
        {
            // Arrange
            var createDto = new CreateCategoryRequestDto { Name = "Test Category" };
            var category = new Category { Id = 1, Name = createDto.Name };

            _businessRulesMock.Setup(b => b.CategoryNameUniqueCheck(createDto.Name)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<Category>(createDto)).Returns(category);
            _categoryRepositoryMock.Setup(r => r.AddAsync(category)).ReturnsAsync(category);

            // Act
            var result = await _categoryService.AddAsync(createDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Category Added.", result.Message);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnCategories_WhenCategoriesExist()
        {
            // Arrange
            var categories = new List<Category> { new Category { Id = 1, Name = "Category 1" }, new Category { Id = 2, Name = "Category 2" } };
            var responseDtos = new List<CategoryResponseDto> { new CategoryResponseDto { Id = 1, Name = "Category 1" }, new CategoryResponseDto { Id = 2, Name = "Category 2" } };

            _categoryRepositoryMock.Setup(r => r.GetAllAsync(null, false)).ReturnsAsync(categories);
            _mapperMock.Setup(m => m.Map<List<CategoryResponseDto>>(categories)).Returns(responseDtos);

            // Act
            var result = await _categoryService.GetAllAsync();

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(responseDtos, result.Data);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            int categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Category" };
            var responseDto = new CategoryResponseDto { Id = categoryId, Name = "Category" };

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(categoryId)).ReturnsAsync(category);
            _businessRulesMock.Setup(b => b.CategoryIsNullCheck(category));
            _mapperMock.Setup(m => m.Map<CategoryResponseDto>(category)).Returns(responseDto);

            // Act
            var result = await _categoryService.GetByIdAsync(categoryId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Category Found", result.Message);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(responseDto, result.Data);
        }

        [Test]
        public async Task RemoveAsync_ShouldReturnSuccess_WhenCategoryIsRemoved()
        {
            // Arrange
            int categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Category" };

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(categoryId)).ReturnsAsync(category);
            _businessRulesMock.Setup(b => b.CategoryIsNullCheck(category));
            _categoryRepositoryMock.Setup(r => r.AddAsync(category)).ReturnsAsync(category);

            // Act
            var result = await _categoryService.RemoveAsync(categoryId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Category Deleted", result.Message);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnSuccess_WhenCategoryIsUpdated()
        {
            // Arrange
            var updateDto = new UpdateCategoryRequestDto { Id = 1, Name = "Updated Category" };
            var category = new Category { Id = updateDto.Id, Name = "Old Category" };

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(updateDto.Id)).ReturnsAsync(category);
            _businessRulesMock.Setup(b => b.CategoryIsNullCheck(category));
            _businessRulesMock.Setup(b => b.CategoryNameUniqueCheck(updateDto.Name)).Returns(Task.CompletedTask);

            // Act
            var result = await _categoryService.UpdateAsync(updateDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Category Updated.", result.Message);
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}
