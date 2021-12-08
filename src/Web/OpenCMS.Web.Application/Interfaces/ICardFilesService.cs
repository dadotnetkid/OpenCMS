using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Shared.Models;

namespace OpenCMS.Web.Application.Interfaces
{
    public interface ICardFilesService
    {
        Task<BaseResponse> Delete(CardFilesModel context);
    }
}
