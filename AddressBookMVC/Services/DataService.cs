using AddressBookMVC.Data;
using AddressBookMVC.Models;
using AddressBookMVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookMVC.Services
{
    //responsible for making sure there is a least one user and some data in the database. 
    public static class DataService
    {
        public static async Task ManageDataAsync(IHost host)
        {
            using var svcScope = host.Services.CreateScope();
            var svcProvider = svcScope.ServiceProvider;
            //Database Access
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();
            //UseManager to create a default user
            var userManagerSvc = svcProvider.GetRequiredService<UserManager<AppUser>>();
            //Category Service to add new contacts to categories
            var categorySvc = svcProvider.GetRequiredService<ICategoryService>();
            //for published applicatoins - this runs update-database on production code
            await dbContextSvc.Database.MigrateAsync();

            //Custom Address book seeding methods
            await SeedDefaultUserAsync(userManagerSvc);
            await SeedDefaultContacts(dbContextSvc);
            await SeedDefaultCategoriesAsync(dbContextSvc);
            await DefaultCategoryAssign(categorySvc, dbContextSvc);

        }

        private async static Task SeedDefaultUserAsync(UserManager<AppUser> userManagerSvc)
        {
            var defaultUser = new AppUser
            {
                UserName = "tonystark@mailinator.com",
                Email = "tonystark@mailinator.com",
                FirstName = "Tony",
                LastName = "Stark",
                EmailConfirmed = true,

            };
            try
            {
                var user = await userManagerSvc.FindByNameAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc123!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("...ERROR....");
                Console.WriteLine("Error Seeding Default User");
                Console.WriteLine(ex.Message);
                Console.WriteLine("...END OF ERROR...");
            }

        }

        private static async Task SeedDefaultContacts(ApplicationDbContext dbContextSvc)
        {
            var userId = dbContextSvc.Users.FirstOrDefault(u => u.Email == "tonystark@mailinator.com").Id;
            var defaultContact = new Contact
            {
                UserId = userId,
                Created = DateTime.Now,
                FirstName = "John",
                LastName = "Doe",
                Address1 = "1234 Memory Lane",
                City = "Houston",
                Phone = "555-555-5555",
                State = enums.States.TX,
                Email = "johhnyD@mailenator.com"
            };
            try
            {
                var contact = await dbContextSvc.Contacts.AnyAsync(c => c.Email == "johhnyD@mailenator.com" && c.UserId == userId);
                if (!contact)
                {
                    await dbContextSvc.AddAsync(defaultContact);
                    await dbContextSvc.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("...ERROR....");
                Console.WriteLine("Error Seeding Default Contact");
                Console.WriteLine(ex.Message);
                Console.WriteLine("...END OF ERROR...");
            }
        }

        private async static Task SeedDefaultCategoriesAsync(ApplicationDbContext dbContextSvc)
        {
            var userId = dbContextSvc.Users.FirstOrDefault(u => u.Email == "tonystark@mailinator.com").Id;

            var defaultCategory = new Category
            {
                UserId = userId,
                Name = "Test"
            };
            try
            {
                var category = await dbContextSvc.Categories.AnyAsync(c => c.Name == "Test" && c.UserId == userId);
                if (!category)
                {
                    await dbContextSvc.AddAsync(defaultCategory);
                    await dbContextSvc.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("...ERROR....");
                Console.WriteLine("Error Seeding Default User");
                Console.WriteLine(ex.Message);
                Console.WriteLine("...END OF ERROR...");
            }




        }

        private async static Task DefaultCategoryAssign(ICategoryService categorySvc, ApplicationDbContext dbContextSvc)
        {
            var user = dbContextSvc.Users
                .Include(c => c.Categories)
                .Include(c => c.Contacts)
                .FirstOrDefault(u => u.Email == "tonystark@mailinator.com");

            foreach (var contact in user.Contacts)
            {
                foreach (var category in user.Categories)
                {
                    await categorySvc.AddContactToCategoryAsync(category.Id, contact.Id);
                }
            }

        }

    }

}

