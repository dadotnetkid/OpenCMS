using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenCMS.Web.Application.Interfaces;
using OpenCMS.Web.Infrastructure.Models;
using System.Net.Http.Json;
using MudBlazor;
using OpenCMS.Shared.Common;
using OpenCMS.Shared.Models;
using OpenCMS.Web.Application.ApiClients;

namespace OpenCMS.Web.Application.Services
{
    public class CardFilesService : ICardFilesService
    {
        private readonly ICardFilesApiClient _cardFilesApiClient;
        private readonly IDialogService _dialogService;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IUserService _userService;

        public CardFilesService(ICardFilesApiClient cardFilesApiClient, IDialogService dialogService)
        {
            _cardFilesApiClient = cardFilesApiClient;
            _dialogService = dialogService;
        }

        public async Task<BaseResponse> Delete(CardFilesModel context)
        {
            var res = await _dialogService.ShowMessageBox($"Delete {context.FullName}", $"Do you want this card file {context.FullName}", "Continue", "Cancel");
            if (res == true)
                return await _cardFilesApiClient.Delete(context.Id);
            else
                return new BaseResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest };
        }
    }
}
