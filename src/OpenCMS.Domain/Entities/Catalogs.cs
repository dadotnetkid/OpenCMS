using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Domain.Entities
{
    [Table("Catalogs")]
    public class Catalogs : BaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(128)]
        public string ItemNumber { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }
        public bool? IBuyThisItem { get; set; }
        public bool? ISellThisItem { get; set; }
        public bool? IInventoryThisItem { get; set; }
        [MaxLength(128)]
        public string SalesAcount { get; set; }
        [MaxLength(128)]
        public string IncomeAcount { get; set; }
        [MaxLength(128)]
        public string InventoryAccount { get; set; }
        public decimal LastCost { get; set; }
        public decimal SellPrice { get; set; }
        public decimal Quantity { get; set; }
    }

}
