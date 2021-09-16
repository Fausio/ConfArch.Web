using ConfArch.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfArch.Web.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public  IActionResult  Login(string returnUrl = "/") =>    View(new LoginModel { ReturnUrl = returnUrl });

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = userre

            return    null;
        }

    }
}
