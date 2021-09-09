using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PushToGa.Web.Helpers;
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
        private readonly IConfiguration configuration;
        public LoginController(ILoginService loginService, IConfiguration configuration)
        {
            this.loginService = loginService;
            this.configuration = configuration;
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
            var trackingId = configuration.GetSection("TrackingId").Value;
            
            var result = await loginService.LoginAsync(model.Username, model.Password);
            if (result != null)
            {
                if (result.Status == "Active")
                {
                    var googleAnalyticsHelper = new GoogleAnalyticsHelper(trackingId, result.Id.ToString());
                    var response = googleAnalyticsHelper.TrackEvent("Authentication",
                        "Login",
                        $"User ID - { result.Id.ToString() } " +
                        $"- { result.Username } " +
                        $"- Internal ({ result.IsInternalUser })").Result;                    

                    if (!response.IsSuccessStatusCode)
                    {
                        new Exception("something went wrong");
                    }
                    ViewBag.UserId = result.Id.ToString();

                    return Redirect("~/Home/Index");
                }
                else if (result.Status == "NoMatch")
                    ViewBag.LoginMsg = "Invalid Password";
                else if (result.Status == "NoUser")
                    ViewBag.LoginMsg = "Invalid User name";
                else if (result.Status == "APIError")
                    ViewBag.LoginMsg = "Oooooops, Service Error";
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
    }
}
