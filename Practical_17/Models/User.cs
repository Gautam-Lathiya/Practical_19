using Microsoft.AspNetCore.Identity;

namespace Practical_17.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
