using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Application.Interfaces.Services;
using OpenCMS.Domain.Entities;

namespace OpenCMS.Application.Services
{
    public class AccountsService:IAccountsService
    {
        private readonly IRepository<Accounts, string> _accountRepo;

        public AccountsService(IRepository<Accounts, string> accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public List<Accounts> List(int categoryNumber, string parentId)
        {
            var list = new List<Accounts>();

            return list;
        }
    }
}
