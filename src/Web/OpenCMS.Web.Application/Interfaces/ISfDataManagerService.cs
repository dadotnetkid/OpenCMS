using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Shared.Models;

namespace OpenCMS.Web.Application.Interfaces
{
    public interface ISfDataManagerService
    {
        public SfDataManagerModel Get();
    }
}
