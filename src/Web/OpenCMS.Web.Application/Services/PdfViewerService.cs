using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using OpenCMS.Web.Application.Interfaces;

namespace OpenCMS.Web.Application.Services
{
    public class PdfViewerService:IPdfViewerService
    {
        private readonly IJSRuntime _jSRuntime;

        public PdfViewerService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task RenderPdf(string htmlElement, string url)
        {
            await _jSRuntime.InvokeVoidAsync("ShowPdfViewer",htmlElement,url);
        }
    }
}
