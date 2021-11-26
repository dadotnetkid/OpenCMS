﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenCMS.Web.Application.Interfaces;
using OpenCMS.Web.Infrastructure.Models;
using System.Net.Http.Json;
using OpenCMS.Shared.Common;
using OpenCMS.Shared.Models;

namespace OpenCMS.Web.Application.Services
{
    public class CardFilesService:ICardFilesService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IUserService _userService;

        public CardFilesService(IHttpClientFactory clientFactory,IUserService userService)
        {
            _clientFactory = clientFactory;
            _userService = userService;
            
            
        }
        public async Task<PaginatedBaseResponse<List<CardFilesModel>>> GetAll(CardFileType cardFileType)
        {
            var _http = _clientFactory.CreateClient("OpenCMS");
            _http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _userService.TokenResponse?.AccessToken);
            var get =await _http.GetFromJsonAsync<PaginatedBaseResponse<List<CardFilesModel>>>($"cardfiles/{(int)cardFileType}");
            return get;
        }

        public async Task Delete(CardFilesModel item)
        {
            var _http = _clientFactory.CreateClient("OpenCMS");
            _http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _userService.TokenResponse?.AccessToken);
            var delete= await _http.DeleteAsync($"cardfiles/{item.Id}");
        }

        public async Task<BaseResponse> CreateOrUpdate(CardFilesModel model)
        {
            var _http = _clientFactory.CreateClient("OpenCMS");
            _http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _userService.TokenResponse?.AccessToken);
            var contentString = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var post= await _http.PostAsync($"cardfiles",contentString);
            BaseResponse result =JsonSerializer.Deserialize<ErrorResponse>(await post.Content.ReadAsStringAsync());
            if(result .HttpStatusCode==System.Net.HttpStatusCode.BadRequest)
            {
                return result;
            }
            result = JsonSerializer.Deserialize<BaseResponse<CardFilesModel>>(await post.Content.ReadAsStringAsync());
            return result;
        }
    }
}
