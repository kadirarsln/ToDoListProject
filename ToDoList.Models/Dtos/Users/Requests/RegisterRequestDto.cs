
namespace ToDoList.Models.Dtos.Users.Requests;

public record RegisterRequestDto
(
    string FirstName,
string LastName,
string Email,
string Password,
string Username
);

