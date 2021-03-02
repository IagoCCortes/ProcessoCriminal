using Microsoft.AspNetCore.Http;
using ProcedimentoCriminal.Core.Application.Interfaces;

namespace ProcedimentoCriminal.Reportacao.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.Request?.Headers["user"];
        }

        public string UserId { get; set; }
    }
}