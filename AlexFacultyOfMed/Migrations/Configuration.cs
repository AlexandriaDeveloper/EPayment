using System;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using AlexFacultyOfMed.Controllers;
using AlexFacultyOfMed.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AlexFacultyOfMed.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{

            //    System.Diagnostics.Debugger.Launch();

            //}
            FillData();
        }


        private async void FillData()
        {
            try
            {
                AspNetUser user = null;
                var store = new UserStore<AspNetUser>(ApplicationDbContext.Create());
                var manager = new ApplicationUserManager(store);
                var adminUser = manager.FindByEmailAsync("seaagull@hotmail.com").Result;


                if (adminUser == null)
                {
                    user = new AspNetUser
                    {
                        UserName = "seaagull@hotmail.com",
                        Email = "seaagull@hotmail.com",
                        NationalId = "28505190201376",
                        Code = 2309,
                        PhoneNumber = "01111269832",
                        EmailConfirmed = true
                    };
                    //var role1 = new IdentityRole { Name = UserRoles.Admin };
                    //var role2 = new IdentityRole { Name = UserRoles.PowerUser };
                    //var role3 = new IdentityRole { Name = UserRoles.User };

                    //user.Roles.Add(new IdentityUserRole { RoleId = role1.Id, UserId = user.Id });
                    //user.Roles.Add(new IdentityUserRole { RoleId = role2.Id, UserId = user.Id });
                    //user.Roles.Add(new IdentityUserRole { RoleId = role3.Id, UserId = user.Id });
                    var s = manager.CreateAsync(user, "fr33tim3#").Result;
                }
                ;
                var rolestore = new RoleStore<IdentityRole>(ApplicationDbContext.Create());
                var rolemanager = new ApplictaionRoleManager(rolestore);
                var roles = rolemanager.FindByNameAsync(UserRoles.Admin).Result;
                IdentityRole role1 = null;
                IdentityRole role2 = null;
                IdentityRole role3 = null;
                if (roles == null)
                {
                    role1 = new IdentityRole {Name = UserRoles.Admin};
                    role2 = new IdentityRole {Name = UserRoles.PowerUser};
                    role3 = new IdentityRole {Name = UserRoles.User};
                    var resultrole1 = rolemanager.CreateAsync(role1).Result;
                    var resultrole2 = rolemanager.CreateAsync(role2).Result;
                    var resultrole3 = rolemanager.CreateAsync(role3).Result;
                }

                var userrole = new IdentityUserRole
                {
                    UserId = user.Id,
                    RoleId = role1.Id
                };

                if (!user.Roles.Contains(userrole))
                {
                    var userruleresult = manager.AddToRoleAsync(user.Id, role1.Name).Result;
                    var userruleresult2 = manager.AddToRoleAsync(user.Id, role2.Name).Result;
                    var userruleresult3 = manager.AddToRoleAsync(user.Id, role3.Name).Result;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }
    }
}