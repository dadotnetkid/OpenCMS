using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Interfaces
{
    public interface ICatalogsService
    {
        public Task<PaginatedBaseItems<List<CatalogModel>>> GetAll();
    }
}
