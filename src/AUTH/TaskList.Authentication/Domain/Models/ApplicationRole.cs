using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;

namespace TaskList.Authentication.Domain.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string name)
        {
            Name = name;
        }
    }
}
