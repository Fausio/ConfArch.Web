using ConfArch.Data.Models;
using ConfArch.Data.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConfArch.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Private variables
        private IUserRepository _userRepository;
        #endregion

        #region Construtor
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        [AllowAnonymous]
        public  IActionResult  Login(string returnUrl = "/") =>    View(new LoginModel { ReturnUrl = returnUrl });

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = _userRepository.GetByUsernameAndPassword(model.UserName, model.PassWord);
            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
           {
               new Claim (ClaimTypes.NameIdentifier , user.Id.ToString()),
               new Claim(ClaimTypes.Name, user.Name),
               new Claim(ClaimTypes.Role, user.Role),
               new Claim("FavoriteColor", user.FavoriteColor)
           };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = model.RememberLogin });

            return LocalRedirect(model.ReturnUrl);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

    }
}
