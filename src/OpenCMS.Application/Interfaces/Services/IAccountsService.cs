using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Domain.Entities;

namespace OpenCMS.Application.Interfaces.Services
{
    public interface IAccountsService
    {
        public List<Accounts> List(int categoryNumber, string parentId);
    }
}
