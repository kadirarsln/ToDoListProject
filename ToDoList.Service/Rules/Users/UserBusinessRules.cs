using Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using ToDoList.Models.Entities;

namespace ToDoList.Service.Rules.Users
{
    public sealed class UserBusinessRules
    {

        public void CheckForIdentityResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new BusinessException(result.Errors.ToList().First().Description);
            }
        }

        public void UserIsNullCheck(User user)
        {
            if (user is null)
            {
                throw new NotFoundException("İlgili Kulnlanıcı Bulunamadı");
            }
        }
    }
}
