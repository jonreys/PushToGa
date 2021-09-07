using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PushToGa.Web.Models;
using PushToGa.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PushToGa.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService loginService;
        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }
                
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountFormModel model)
        {            
            var result = await loginService.LoginAsync(model.Username, model.Password);
            if (result != null)
            {
                if (result.Status == "Active")
                {
                    if (result.IsOperator.HasValue && result.IsOperator.Value == false)
                    {
                        ViewBag.Name = result.Fullname;
                        ViewBag.Initial = this.GetNameInitials(result.Fullname);
                        ViewBag.UserId = result.Id;
                        await StoreAuthentication(result);
                        return Redirect("~/Home/Index");
                    }
                }
                else if (result.Status == "LockedOut")
                    return Redirect("~/Account/AccountLocked");
                else if (result.Status == "NoMatch")
                    ViewBag.LoginMsg = "Invalid Password";
                else if (result.Status == "NoUser")
                    ViewBag.LoginMsg = "Invalid User name";
                return View("Login");
            }
            else
            {
                ViewBag.LoginMsg = "Invalid Account";
                return View("Login");
            }
        }

        private async Task<string> GetNameInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            string[] nameSplit = name.Trim().Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
            var initials = nameSplit[0].Substring(0, 1).ToUpper();

            if (nameSplit.Length > 1)
            {
                initials += nameSplit[nameSplit.Length - 1].Substring(0, 1).ToUpper();
            }
            await Task.CompletedTask;

            return initials;
        }

        private async Task<bool> StoreAuthentication(UserOutput result)
        {
            var authenticated = false;
            //store Token
            HttpContext.Session.SetString("UserToken", result.Token);
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, result.Username),
                        new Claim("FullName", result.Fullname),
                    };

            if (result.Roles.Any())
            {
                result.Roles.ForEach(e =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, e));
                });
            }

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            authenticated = true;

            return authenticated;
        }


    }
}
