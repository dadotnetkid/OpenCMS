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
        public Catalogs()
        {
            this.CatalogSellingDetails = new HashSet<CatalogSellingDetails>();
            this.CatalogBuyingDetails = new HashSet<CatalogBuyingDetails>();
        }
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
        public string ManufacturerNo { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CatalogBuyingDetails> CatalogBuyingDetails { get; set; }
        public virtual ICollection<CatalogSellingDetails> CatalogSellingDetails{ get; set; }
    }
    [Table("CatalogSellingDetails")]
    public class CatalogSellingDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CatalogId { get; set; }
        public decimal BaseSellingPrice { get; set; }
        [MaxLength(50)]
        public string UOM { get; set; }
        public int SellingUnit { get; set; }
        public decimal Retail { get; set; }
        public decimal WholeSale { get; set; }
        public bool IsActive { get; set; }
        public DateTime? AppliedStartDate { get; set; }

        public virtual Catalogs Catalog { get; set; }
    }
    [Table("CatalogBuyingDetails")]
    public class CatalogBuyingDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? CatalogId { get; set; }
        public decimal StandardCost { get; set; }
        [MaxLength(50)]
        public string UOM { get; set; }
        public int BuyingUnit { get; set; }
        public decimal MinimumLevelRestockingAlert { get; set; }
        public decimal DefaultReorderQuantity { get; set; }
        public DateTime AppliedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Catalogs Catalog { get; set; }
    }
}
