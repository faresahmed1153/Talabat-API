using Microsoft.AspNetCore.Identity;
using Talabat.Core.Models.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Fares Ahmed",
                    Email = "faresahmed@gmail.com",
                    UserName = "faresahmed",
                    PhoneNumber = "0111122233445"
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }

        }
    }
}
