﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Web.Infrastructure.Common;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Interfaces
{
    public interface ICardFilesService
    {
        public Task<PaginatedBaseResponse<List<CardFilesModel>>> GetAll(CardFileType cardFileType);
        Task Delete(CardFilesModel item);
    }
}