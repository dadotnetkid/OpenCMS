using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Domain.Entities
{
    [Table("Inventories")]
    public class Inventories:BaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? CatalogId { get; set; }
        public int? InventoryType { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("CatalogId")]
        public virtual Catalogs Catalog { get; set; }
    }

}
