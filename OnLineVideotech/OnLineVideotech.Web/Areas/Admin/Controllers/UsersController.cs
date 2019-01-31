﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnLineVideotech.Data.Models;
using OnLineVideotech.Services.Admin.Interfaces;
using OnLineVideotech.Services.Admin.Models;
using OnLineVideotech.Web.Areas.Admin.Models.Users;
using OnLineVideotech.Web.Infrastructure;
using OnLineVideotech.Web.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnLineVideotech.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class UsersController : Controller
    {
        private readonly IAdminUserService users;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IAdminUserService users,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            this.users = users;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AdminUserListingServiceModel> users = await this.users.AllAsync();

            IEnumerable<SelectListItem> roles = await this.roleManager
                .Roles
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name
                })
                .ToListAsync();

            return View(new AdminUserListingsViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
        {
            bool roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            User user = await this.userManager.FindByIdAsync(model.UserId);
            bool userExists = user != null;

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            TempData.AddSuccessMessage($"User {user.UserName} successfully added to {model.Role} role");

            return RedirectToAction(nameof(Index));
        }
    }
}