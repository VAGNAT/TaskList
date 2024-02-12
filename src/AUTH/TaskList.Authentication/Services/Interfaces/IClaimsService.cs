using System.Security.Claims;
using TaskList.Authentication.Domain.Models;

namespace TaskList.Authentication.Services.Interfaces
{
    public interface IClaimsService
    {
        Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user);
    }
}
