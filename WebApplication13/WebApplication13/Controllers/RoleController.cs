﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication13.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        //[Authorize(Policy = "rolecreation")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            //var roles = rolesWithAdmin.Find(x=>x.Name != "Admin");
            return View(roles);
        }

        [Authorize(Policy = "rolecreation")]
        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        [Authorize(Policy = "rolecreation")]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
    }
}
