using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Web.Application.Interfaces;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Services
{
    public class CatalogsService:ICatalogsService
    {
        private readonly IOpenCMSHttpClient _openCmsHttp;

        public CatalogsService(IOpenCMSHttpClient openCMSHttp)
        {
            _openCmsHttp = openCMSHttp;
        }
        public async Task<PaginatedBaseItems<List<CatalogModel>>> GetAll()
        {
            var http = _openCmsHttp.Create();
            var items= await http.GetFromJsonAsync<PaginatedBaseResponse<List<CatalogModel>>>("catalogs");
            return items.Data;
        }
    }
}
