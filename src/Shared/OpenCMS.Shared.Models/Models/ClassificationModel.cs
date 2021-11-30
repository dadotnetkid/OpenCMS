using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Domain.Models;

namespace OpenCMS.Shared.Models
{
    public class ClassificationModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal TotalTransaction { get; set; }
        public decimal RunningBalance { get; set; }
        public int OrderBy { get; set; }
        public virtual ICollection<AccountModel> Accounts { get; set; }
    }
}
