using Microsoft.AspNetCore.Identity;

namespace Microservices.IDP.Infrastructure.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }


    }
}
