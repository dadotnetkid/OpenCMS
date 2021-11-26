using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Domain.Entities
{
    public class BaseEntity<TKey>
    {
        public int TenantId { get; set; }
        public bool Deleted { get; set; }
    }
}
