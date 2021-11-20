using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Domain.Models.InputModels
{
    public class CreateCatalogInputModel
    {
        public string ItemNumber { get; set; }
        public string Name { get; set; }
        public bool? IBuyThisItem { get; set; }
        public bool? ISellThisItem { get; set; }
        public bool? IInventoryThisItem { get; set; }
        public string SalesAccount { get; set; }
        public string IncomeAccount { get; set; }
        public string InventoryAccount { get; set; }
        public string ManufacturerNo { get; set; }
        public string Description { get; set; }
    }
}
