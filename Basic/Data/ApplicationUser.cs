using Microsoft.AspNetCore.Identity;

namespace Basic.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName {get;set;}
    }
}