using System.Security.Claims;

namespace StackoveflowClone.Services
{
    public interface IUserHttpContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }
}