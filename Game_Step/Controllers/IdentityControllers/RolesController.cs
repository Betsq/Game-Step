using Game_Step.IdentityViewModels;
using Game_Step.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Controllers.IdentityControllers
{
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<User> userManager;

        public RolesController(RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(roleManager.Roles.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            return View(await userManager.Users.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            //Get user
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                //Get roles of user list
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = await roleManager.Roles.ToListAsync();

                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles,
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                //Getting the list of user roles
                var userRoles = await userManager.GetRolesAsync(user);

                var allRoles = await roleManager.Roles.ToListAsync();

                //Getting the list of roles that has been added 
                var addedRoles = roles.Except(userRoles);

                //Getting the list of roles that has been romoved 
                var removedRoles = userRoles.Except(roles);

                await userManager.AddToRolesAsync(user, addedRoles);

                await userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
