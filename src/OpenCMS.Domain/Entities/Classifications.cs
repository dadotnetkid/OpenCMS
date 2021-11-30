using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Domain.Entities
{
    public class Classifications : BaseEntity<string>
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal TotalTransaction { get; set; }
        public decimal RunningBalance { get; set; }
        public int OrderBy { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
    }
}
