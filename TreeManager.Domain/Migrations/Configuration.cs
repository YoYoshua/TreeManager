namespace TreeManager.Domain.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TreeManager.Domain.Concrete;
    using TreeManager.Domain.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<TreeManager.Domain.Concrete.AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppIdentityDbContext context)
        {
            //seed admin
            AppUserManager userMgr = new AppUserManager(new UserStore<User>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<Role>(context));

            string roleName = "Admin";

            string userName = "Admin";
            string password = "AdminTest";
            string email = "admin@admin.com";

            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new Role(roleName));
            }

            User user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new User { UserName = userName, Email = email }, password);
                user = userMgr.FindByName(userName);
            }

            if(!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }

            base.Seed(context);
        }
    }
}
