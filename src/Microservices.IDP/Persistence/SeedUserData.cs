using IdentityModel;
using Microservices.IDP.Common;
using Microservices.IDP.Infrastructure.Common;
using Microservices.IDP.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Microservices.IDP.Persistence
{
    public class SeedUserData
    {
        public static void EnsureSeedData(string connectionString)
        {

            var servies = new ServiceCollection();
            servies.AddLogging();
            servies.AddDbContext<IdentityContext>(opt => opt.UseSqlServer(connectionString));

            servies.AddIdentity<User, IdentityRole>(opt => {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;

            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();


            using (var serviceProvider = servies.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {

                    CreateUser(scope, "Alice", "Smith", "Alice Smith's Go Vap", Guid.NewGuid().ToString(), "alice123", "Administrator", "alicesmith@example.com");

                }
            }


        }

        private static void CreateUser(IServiceScope scope, string firstName, string lastName, string address, string id, string password, string role, string email)
        {

            var userManagment = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            var user = userManagment.FindByNameAsync(email).Result;

            if (user == null)
            {
                user = new User
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    Id = id,
                };

                var result = userManagment.CreateAsync(user, password).Result;
                CheckResult(result);


                var addToRoleResult = userManagment.AddToRoleAsync(user, role).Result;
                CheckResult(addToRoleResult);


                result = userManagment.AddClaimsAsync(user, new Claim[] 
                {
                    new(SystemConstants.Claims.UserName,user.UserName),
                    new(SystemConstants.Claims.FirstName,user.FirstName),
                    new(SystemConstants.Claims.LastName,user.LastName),
                    new(SystemConstants.Claims.Roles,role),
                    new(JwtClaimTypes.Address,user.Address),
                    new(JwtClaimTypes.Email,user.Email),
                    new(ClaimTypes.NameIdentifier,user.Id)
                }).Result;
                CheckResult(result);




            }

        }

        private static void CheckResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }


    }
}
