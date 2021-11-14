using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Application.Interfaces.Services;
using OpenCMS.Domain.Entities;
using OpenCMS.Infrastructure.Common;

namespace OpenCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CardFilesController : ControllerBase
    {
        private readonly IRepository<CardFiles, int> _cardFileService;
        private readonly IUserService _userService;

        public CardFilesController(IRepository<CardFiles, int> customerRepo, IUserService cardFileService)
        {
            _cardFileService = customerRepo;
            _userService = cardFileService;
        }
        [HttpGet("{cardFileType}")]
        public IActionResult Get(int cardFileType)
        {
            var userId = _userService.GetUserId();
            return Ok(new PaginatedResponse<object>()
            {
                Data = new PaginateItems<object>()
                {
                    Items = _cardFileService.GetAll(x => x.CreatedBy == userId && x.CardFileType==cardFileType),
                    Total = _cardFileService.GetAll(x => x.CreatedBy == userId && x.CardFileType == cardFileType).Count()
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
