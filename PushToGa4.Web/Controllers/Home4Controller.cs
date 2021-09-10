using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushToGa4.Web.Controllers
{
    public class Home4Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
