namespace LoganBoyter.Data.Migrations
{
    using LoganBoyter.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LoganBoyter.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LoganBoyter.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var RoleStore = new RoleStore<IdentityRole>(context);
            var RoleManager = new RoleManager<IdentityRole>(RoleStore);

            if (!RoleManager.RoleExists("Admin"))
            {
                var Role = new IdentityRole();
                Role.Name = "Admin";
                RoleManager.Create(Role);
            }
            if (!RoleManager.RoleExists("User"))
            {
                var Role = new IdentityRole();
                Role.Name = "User";
                RoleManager.Create(Role);
            }

            var UserStore = new UserStore<ApplicationUser>(context);
            var UserManager = new UserManager<ApplicationUser>(UserStore);

            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                ApplicationUser User = new ApplicationUser();
                User.UserName = "Admin";
                UserManager.Create(User, "asdfasdf");
                UserManager.AddToRole(User.Id, "Admin");
            }

            context.Skills.AddOrUpdate(
                c => c.Title,
                new Skill { Title = "HTML", Description = "While design is not Logan's strong suit, he is proficient HTML.", Icon = "html5" },
                new Skill { Title = "CSS", Description = "While design is not Logan's strong suit, he is proficient in CSS.", Icon = "css3" },
                new Skill { Title = "JavaScript", Description = "Logan is experienced with JavaScript and AngularJS", Icon = "jsfiddle" },
                new Skill { Title = "C#", Description = "Logan is skillfull in C#, including MVC, Web API, and Entity Framework", Icon = "stack-overflow" }
                );
        }
    }
}
