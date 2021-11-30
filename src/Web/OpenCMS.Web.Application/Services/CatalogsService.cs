using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenCMS.Shared.Models;
using OpenCMS.Web.Application.Interfaces;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Services
{
    public class CatalogsService : ICatalogsService
    {
        private readonly IOpenCMSHttpClient _openCmsHttp;

        public CatalogsService(IOpenCMSHttpClient openCMSHttp)
        {
            _openCmsHttp = openCMSHttp;
        }
        public async Task<PaginatedBaseItems<List<CatalogModel>>> GetAll()
        {
            var http = _openCmsHttp.Create();
            var items = await http.GetFromJsonAsync<PaginatedBaseResponse<List<CatalogModel>>>("catalogs");
            return items.Data;
        }

        public async Task CreateOrUpdate(CatalogModel model)
        {
            var http = _openCmsHttp.Create();
            var contentString = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var createOrUpdate = model.Id == 0 ? await http.PostAsync("catalogs", contentString)
                : await http.PatchAsync("catalogs", contentString);
            var content = await createOrUpdate.Content.ReadAsStringAsync();

        }

        public async Task Delete(int contextId)
        {
            var http = _openCmsHttp.Create();
            await http.DeleteAsync($"catalogs/{contextId}");
        }
    }
}
