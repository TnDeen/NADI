namespace MVC5.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC5.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "MVC5.Models.ApplicationDbContext";
        }

        protected override void Seed(MVC5.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
           


            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                
                var userRole = new IdentityRole { Name = "User" };

                manager.Create(userRole);
            }

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var Adminrole = new IdentityRole { Name = "Admin" };

                manager.Create(Adminrole);
            }

            if (!context.Users.Any(u => u.UserName == "admin@nadikebangsaan.com"))
            {
                var userstore = new UserStore<ApplicationUser>(context);
                var usermanager = new UserManager<ApplicationUser>(userstore);
                var user = new ApplicationUser { UserName = "admin@nadikebangsaan.com", Email = "admin@nadikebangsaan.com", NomborAhli = "NDAD20170001" };

                usermanager.Create(user, "Nadiadmin1@");
                usermanager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
