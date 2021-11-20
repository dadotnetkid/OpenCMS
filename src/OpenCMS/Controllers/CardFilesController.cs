using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Application.Interfaces.Services;
using OpenCMS.Domain.Entities;
using OpenCMS.Infrastructure.Common;
using OpenCMS.Shared.Models;

namespace OpenCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardFilesController : ControllerBase
    {
        private readonly IRepository<CardFiles, int> _cardFileService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CardFilesController(IRepository<CardFiles, int> customerRepo, IUserService cardFileService,IMapper mapper)
        {
            _cardFileService = customerRepo;
            _userService = cardFileService;
            _mapper = mapper;
        }
        [HttpGet("{cardFileType}")]
        public IActionResult Get(int cardFileType)
        {
            var userId = _userService.GetUserId();
            var item = _cardFileService.Fetch(x => x.CreatedBy == userId && x.CardFileType == cardFileType);
            var model = _mapper.ProjectTo<CardFilesModel>(item);
            return Ok(new PaginatedResponse<object>()
            {
                Data = new PaginateItems<object>()
                {
                    Items =model ,
                    Total = item.Count()
                }
            });
        }

        [HttpDelete("{cardFileId}")]
        public async Task<IActionResult> Delete(int cardFileId)
        {
            await _cardFileService.Delete(cardFileId);
            return Ok(new BaseResponse<object>()
            {
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
    }
}
