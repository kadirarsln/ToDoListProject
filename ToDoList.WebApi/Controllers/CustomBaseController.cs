
using Core.Tokens.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ToDoList.WebApi.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        protected string GetUserId()
        {
            return HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        }
    }
}
