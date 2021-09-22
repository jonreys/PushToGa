using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushToGa4.Web.Controllers
{
    public class MyTechnology : Controller
    {
        [Route("MyTechnology/AssetDetails/da595ba3-e3f6-4d77-9423-e059b8a62884")]
        public IActionResult da595ba3e3f64d779423e059b8a62884()
        {            
            return View();
        }

        [Route("MyTechnology/AssetDetails/Items")]
        public IActionResult Items()
        {
            return View();
        }
    }
}
