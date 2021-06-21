﻿using Microsoft.AspNetCore.Identity;
using PermissionManagement.MVC.Constants;
using System.Threading.Tasks;

namespace PermissionManagement.MVC.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Manager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Responsable_Comercial.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Responsable_Marketing.ToString()));

        }
    }
}