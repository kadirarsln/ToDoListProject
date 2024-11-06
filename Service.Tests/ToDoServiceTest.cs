using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using ToDoList.DataAcces.Abstracts;
using ToDoList.Models.Dtos.ToDos.Requests;
using ToDoList.Models.Dtos.ToDos.Responses;
using ToDoList.Models.Entities;
using Core.Responses;
using ToDoList.Service.Concretes;
using ToDoList.Service.Rules.ToDos;

namespace Service.Tests;

[TestFixture]
public class ToDoServiceTests
{
    private Mock<IToDoRepository> _toDoRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private Mock<ToDosBusinessRules> _businessRulesMock;
    private ToDoService _toDoService;

    [SetUp]
    public void Setup()
    {
        _toDoRepositoryMock = new Mock<IToDoRepository>();
        _mapperMock = new Mock<IMapper>();
        _businessRulesMock = new Mock<ToDosBusinessRules>();
        _toDoService = new ToDoService(_toDoRepositoryMock.Object, _mapperMock.Object, _businessRulesMock.Object);
    }

    [Test]
    public async Task AddAsync_ShouldReturnSuccess_WhenToDoIsValid()
    {
        // Arrange
        var createRequest = new CreateToDoRequest { Title = "Test ToDo", StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(2), CategoryId = 1 };
        var createdToDo = new ToDo { Id = Guid.NewGuid(), UserId = "user1" };
        var responseDto = new ToDoResponseDto { Id = createdToDo.Id };

        _businessRulesMock.Setup(b => b.ToDoTitleCheck(createRequest.Title));
        _mapperMock.Setup(m => m.Map<ToDo>(createRequest)).Returns(createdToDo);
        _mapperMock.Setup(m => m.Map<ToDoResponseDto>(createdToDo)).Returns(responseDto);
        _toDoRepositoryMock.Setup(r => r.AddAsync(It.IsAny<ToDo>())).ReturnsAsync(createdToDo);

        // Act
        var result = await _toDoService.AddAsync(createRequest, "user1");

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual("ToDo Added.", result.Message);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual(responseDto, result.Data);
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnListOfToDos()
    {
        // Arrange
        var todos = new List<ToDo> { new ToDo { Id = Guid.NewGuid() } };
        var responseDtos = new List<ToDoResponseDto> { new ToDoResponseDto { Id = todos[0].Id } };

        _toDoRepositoryMock.Setup(r => r.GetAllAsync(null, false)).ReturnsAsync(todos);
        _mapperMock.Setup(m => m.Map<List<ToDoResponseDto>>(todos)).Returns(responseDtos);

        // Act
        var result = await _toDoService.GetAllAsync();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual(responseDtos, result.Data);
    }

    [Test]
    public async Task GetAllByCategoryIdAsync_ShouldReturnToDosInCategory()
    {
        // Arrange
        var categoryId = 1;
        var todos = new List<ToDo> { new ToDo { CategoryId = categoryId } };
        var responseDtos = new List<ToDoResponseDto> { new ToDoResponseDto() };

        _toDoRepositoryMock.Setup(r => r.GetAllAsync(t => t.CategoryId == categoryId, false)).ReturnsAsync(todos);
        _mapperMock.Setup(m => m.Map<List<ToDoResponseDto>>(todos)).Returns(responseDtos);

        // Act
        var result = await _toDoService.GetAllByCategoryIdAsync(categoryId);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual("ToDos Listed.", result.Message);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual(responseDtos, result.Data);
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnToDo_WhenToDoExists()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        var todo = new ToDo { Id = todoId };
        var responseDto = new ToDoResponseDto { Id = todoId };

        _toDoRepositoryMock.Setup(r => r.GetByIdAsync(todoId)).ReturnsAsync(todo);
        _businessRulesMock.Setup(b => b.ToDoIsNullCheck(todo, todoId));
        _mapperMock.Setup(m => m.Map<ToDoResponseDto>(todo)).Returns(responseDto);

        // Act
        var result = await _toDoService.GetByIdAsync(todoId);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual("ToDo Found", result.Message);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual(responseDto, result.Data);
    }

    [Test]
    public async Task RemoveAsync_ShouldReturnDeletedToDo_WhenToDoExists()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        var todo = new ToDo { Id = todoId };
        var responseDto = new ToDoResponseDto { Id = todoId };

        _toDoRepositoryMock.Setup(r => r.GetByIdAsync(todoId)).ReturnsAsync(todo);
        _toDoRepositoryMock.Setup(r => r.RemoveAsync(todo)).ReturnsAsync(todo);
        _mapperMock.Setup(m => m.Map<ToDoResponseDto>(todo)).Returns(responseDto);

        // Act
        var result = await _toDoService.RemoveAsync(todoId);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual("ToDo Deleted", result.Message);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual(responseDto, result.Data);
    }

    [Test]
    public async Task UpdateAsync_ShouldReturnUpdatedToDo_WhenToDoIsValid()
    {
        // Arrange
        var updateRequest = new UpdateToDoRequest { Id = Guid.NewGuid(), Title = "Updated Title", StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(2), CategoryId = 1 };
        var existingToDo = new ToDo { Id = updateRequest.Id, UserId = "user1" };
        var responseDto = new ToDoResponseDto { Id = updateRequest.Id };

        _toDoRepositoryMock.Setup(r => r.GetByIdAsync(updateRequest.Id)).ReturnsAsync(existingToDo);
        _toDoRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ToDo>())).ReturnsAsync(existingToDo);
        _mapperMock.Setup(m => m.Map<ToDoResponseDto>(existingToDo)).Returns(responseDto);

        // Act
        var result = await _toDoService.UpdateAsync(updateRequest);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual("ToDo Updated", result.Message);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual(responseDto, result.Data);
    }
}