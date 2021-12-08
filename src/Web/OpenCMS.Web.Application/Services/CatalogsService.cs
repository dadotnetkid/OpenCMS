using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MudBlazor;
using OpenCMS.Shared.Models;
using OpenCMS.Web.Application.ApiClients;
using OpenCMS.Web.Application.Interfaces;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Services
{
    public class CatalogsService : ICatalogsService
    {
        private readonly ICatalogsApiClient _catalogsApiClient;
        private readonly IDialogService _dialogService;

        public CatalogsService(ICatalogsApiClient catalogsApiClient,IDialogService dialogService)
        {
            _catalogsApiClient = catalogsApiClient;
            _dialogService = dialogService;
        }
 

        public async Task Delete(int contextId)
        {
            var dialog=await _dialogService.ShowMessageBox("Delete this Catalog", "Do you want to delete this catalog", "Continue",
                "Cancel");
            if (dialog == true)
            {
                await _catalogsApiClient.Delete(contextId);
            }
        }
    }
}
