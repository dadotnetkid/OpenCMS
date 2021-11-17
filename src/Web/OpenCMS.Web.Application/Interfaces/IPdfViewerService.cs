using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Web.Application.Interfaces
{
    public interface IPdfViewerService
    {
        public Task RenderPdf(string htmlElement, string url);
    }
}
