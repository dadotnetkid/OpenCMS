using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace OpenCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PdfFileViewerController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public PdfFileViewerController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var path = System.IO.Path.Combine(_hostEnvironment.ContentRootPath, "Files",
                "HomeTeamNS_API_System_Integration_Document_v2.6.pdf");
            return File(System.IO.File.ReadAllBytes(path), "application/pdf");
        }
    }
}
