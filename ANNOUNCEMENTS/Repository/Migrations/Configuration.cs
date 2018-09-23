namespace Repository.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Repository.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AnnouncementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AnnouncementContext context)
        {
            //For debugging Seed()
            if (System.Diagnostics.Debugger.IsAttached == false)
            {
                System.Diagnostics.Debugger.Launch();
            }

            SeedRoles(context);
            SeedUsers(context);
            SeedAnnouncements(context);
            SeedCategories(context);
            SeedAnnouncement_Category(context);
        }

        private void SeedRoles(AnnouncementContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole()
                {
                    Name = "Admin"
                };
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Pracownik"))
            {
                var role = new IdentityRole()
                {
                    Name = "Pracownik"
                };
                roleManager.Create(role);
            }
        }

        private void SeedUsers(AnnouncementContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);

            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var user = new User { UserName = "Admin" };
                var adminresult = manager.Create(user, "12345678");
                if (adminresult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                }
            }

            if (!context.Users.Any(u => u.UserName == "Marek"))
            {
                var user = new User { UserName = "marek@AspNetMvc.pl" };
                var adminresult = manager.Create(user, "12345678");
                if (adminresult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Pracownik");
                }
            }

            if(!context.Users.Any(u => u.UserName == "Prezes"))
            {
                var user = new User { UserName = "prezes@AspNetMvc.pl" };
                var adminresult = manager.Create(user, "12345678");
                if(adminresult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                }
            }

        }

        private void SeedAnnouncements(AnnouncementContext context)
        {
            var idUser = context.Set<User>().Where(u => u.UserName == "Admin").FirstOrDefault().Id;

            for(int i = 1; i <= 10; i++)
            {
                var announcement = new Announcement()
                {
                    Id = i,
                    UserId = idUser,
                    Content = "Treœæ og³oszenia " + i.ToString(),
                    Title = "Tytu³ og³oszenia " + i.ToString(),
                    DateOfAdd = DateTime.Now.AddDays(-i)
                };
                context.Set<Announcement>().AddOrUpdate(announcement);
            }
            context.SaveChanges();
        }

        private void SeedCategories(AnnouncementContext context)
        {
            for (int i = 1; i <= 10; i++)
            {
                var category = new Category()
                {
                    Id = i,
                    Name = "Nazwa kategorii " + i.ToString(),
                    Content = "Treœæ kategorii " + i.ToString(),
                    MetaDescription = "Opis kategorii " + i.ToString(),
                    MetaTitle = "Tytu³ kategorii " + i.ToString(),
                    MetaWords = "S³owa kluczowe " + i.ToString(),
                    ParentId = i
                };
                context.Set<Category>().AddOrUpdate(category);
            }
            context.SaveChanges();
        }

        private void SeedAnnouncement_Category(AnnouncementContext context)
        {
            for (int i = 1; i <= 10; i++)
            {
                var announcement_category = new Announcement_Category()
                {
                    Id = i,
                    AnnouncementId = i / 2 + 1,
                    CategoryId = i / 2 + 2 
                };
                context.Set<Announcement_Category>().AddOrUpdate(announcement_category);
            }
            context.SaveChanges();
        }
    }

}

