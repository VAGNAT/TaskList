using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TaskList.Authentication.Domain.Models;
using TaskList.Authentication.Services.Interfaces;

namespace TaskList.Authentication.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ClaimsService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user)
        {
            List<Claim> userClaims = new()
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return userClaims;
        }
    }
}
