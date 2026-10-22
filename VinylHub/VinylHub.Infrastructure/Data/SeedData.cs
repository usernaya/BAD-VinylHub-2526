using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Infrastructure.Identity;

namespace VinylHub.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(UserManager<ApplicationUser> userManager)
        {
            // admin 
            var admin = userManager.FindByEmailAsync("admin@vinylhub.be").Result;
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "admin@vinylhub.be",
                    Email = "admin@vinylhub.be",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "VinylHub"
                };
                userManager.CreateAsync(admin, "Admin123!").Wait();
                userManager.AddClaimAsync(admin, new Claim("IsAdmin", string.Empty)).Wait();
            }

            // gewone test user
            var user = userManager.FindByEmailAsync("user@vinylhub.be").Result;
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "user@vinylhub.be",
                    Email = "user@vinylhub.be",
                    EmailConfirmed = true,
                    FirstName = "Test",
                    LastName = "Gebruiker"
                };
                userManager.CreateAsync(user, "User123!").Wait();
            }
        }
    }
}
