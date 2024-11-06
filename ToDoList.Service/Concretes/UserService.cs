using Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using ToDoList.Models.Dtos.Users.Requests;
using ToDoList.Models.Entities;
using ToDoList.Service.Abstracts;
using ToDoList.Service.Rules.Users;

namespace ToDoList.Service.Concretes;

public class UserService(UserManager<User> _userManager, UserBusinessRules businessRules) : IUserService
{
    public async Task<User> ChangePasswordAsync(string id, ChangePasswordRequestDto requestDto)
    {
        var user = await _userManager.FindByIdAsync(id);
        businessRules.UserIsNullCheck(user);

        if (requestDto.NewPassword.Equals(requestDto.NewPasswordAgain) is false)
        {
            throw new ValidationException("Parolanız Uyuşmuyor");
        }
        var result = await _userManager.ChangePasswordAsync(user, requestDto.CurrentPassword, requestDto.NewPassword);
        businessRules.CheckForIdentityResult(result);
        return user;
    }

    public async Task<string> DeleteAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        businessRules.UserIsNullCheck(user);

        var result = await _userManager.DeleteAsync(user);
        businessRules.CheckForIdentityResult(result);
        return "User Deleted";
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var user = await _userManager.FindByIdAsync(email);

        businessRules.UserIsNullCheck(user);
        return user;
    }

    public async Task<User> LoginAsync(LoginRequestDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        businessRules.UserIsNullCheck(user);
        bool checkPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (checkPassword == false)
        {
            throw new BusinessException("Parolanız Hatalı");
        }
        return user;
    }

    public async Task<User> RegisterAsync(RegisterRequestDto dto)
    {
        User user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Username
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        businessRules.CheckForIdentityResult(result);

        var addRole = await _userManager.AddToRoleAsync(user, "User");
        businessRules.CheckForIdentityResult(addRole);

        return user;
    }

    public async Task<User> UpdateAsync(string id, UserUpdateRequestDto dto)
    {
        var user = await _userManager.FindByIdAsync(id);
        businessRules.UserIsNullCheck(user);

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.UserName = dto.UserName;

        var result = await _userManager.UpdateAsync(user);
        businessRules.CheckForIdentityResult(result);
        return user;
    }
}
