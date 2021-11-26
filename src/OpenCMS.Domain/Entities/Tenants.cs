using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Domain.Entities
{
    public class Tenants
    {
        public int Id { get; set; }
        public string Domain { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
