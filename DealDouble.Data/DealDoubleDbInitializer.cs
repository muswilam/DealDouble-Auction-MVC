using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using DealDouble.Entities;

namespace DealDouble.Data
{
    public class DealDoubleDbInitializer : CreateDatabaseIfNotExists<DealDoubleContext> //initialize if db not exists. 
    {
        protected override void Seed(DealDoubleContext context)
        {
            SeedRoles(context);
            SeedUsers(context);
        } 

        public void SeedRoles(DealDoubleContext context)
        {
            var rolesInDealDouble = new List<IdentityRole>();

            rolesInDealDouble.Add(new IdentityRole() { Name = "Admin" });
            rolesInDealDouble.Add(new IdentityRole() { Name = "Moderator" });
            rolesInDealDouble.Add(new IdentityRole() { Name = "User" });

            var rolesStore = new RoleStore<IdentityRole>(context);
            var rolesManager = new RoleManager<IdentityRole>(rolesStore);

            foreach (IdentityRole role in rolesInDealDouble)
            {
                if(!rolesManager.RoleExists(role.Name))
                {
                    var result = rolesManager.Create(role);

                    if (result.Succeeded) continue;
                }
            }
        }

        public void SeedUsers(DealDoubleContext context)
        {
            var usersStore = new UserStore<DealDoubleUser>(context);
            var usersManager = new UserManager<DealDoubleUser>(usersStore);

            var admin = new DealDoubleUser();
            admin.Email = "admin@mail.com";
            admin.UserName = "admin";
            var password = AdminPassword.GetAdminPassword();

            if(usersManager.FindByEmail(admin.Email) == null)
            {
                var result = usersManager.Create(admin, password);

                if(result.Succeeded)
                {
                    //add necessary roles to admin
                    usersManager.AddToRole(admin.Id, "Admin");
                    usersManager.AddToRole(admin.Id, "Moderator");
                    usersManager.AddToRole(admin.Id, "User");
                }
            }
        }
    }
}
