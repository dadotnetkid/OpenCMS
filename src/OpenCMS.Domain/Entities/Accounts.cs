using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Domain.Entities
{
    [Table("Accounts")]
    public class Accounts:BaseEntity<string>
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; }
        [MaxLength(128)]
        public string ParentId { get; set; }
        [MaxLength(128)]
        public string ClassificationId { get; set; }
        public bool? IsHeader { get; set; }
        public bool? IsDetail { get; set; }
        [MaxLength(255)]
        public string Type { get; set; }
        [MaxLength(255)]
        public string AccountName { get; set; }
        public int AccountNumber { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal? CurrentBalance { get; set; }
        public int HierarchyLevel { get; set; }
        public int OrderBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdateOnUtc { get; set; }
        public bool Deleted { get; set; }
        [MaxLength(255)]
        public string CreatedBy { get; set; }
        [MaxLength(255)]
        public string UpdatedBy { get; set; }
    }

}
