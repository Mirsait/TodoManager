namespace TodoManager.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TodoManager.Infrastructure;

    internal sealed class Configuration : DbMigrationsConfiguration<TodoManager.Infrastructure.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TodoManager.Infrastructure.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            var userManager = new UserManager<ApplicationUser> (
                new UserStore<ApplicationUser>(
                    new ApplicationDbContext ()));

            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(
                    new ApplicationDbContext()));
            

            var user = new ApplicationUser()
            {
                UserName = "SuperRoot",
                Email = "mirsait@mail.ru",
                EmailConfirmed = true,
                FirstName = "Frourig",
                LastName = "Mirsait",
                JoinDate = DateTime.Now.AddYears(-3)
            };

            userManager.Create(user, "Mirsait123+++");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Editor" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = userManager.FindByName("SuperRoot");
            userManager.AddToRoles(adminUser.Id,
                new string[]
                {
                    "SuperAdmin",
                    "Admin"
                });
        }
    }
}
