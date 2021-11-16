using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCMS.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }
    }
}
