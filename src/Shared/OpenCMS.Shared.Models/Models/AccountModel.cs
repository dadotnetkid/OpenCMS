using System;
using System.ComponentModel.DataAnnotations;

namespace OpenCMS.Shared.Models
{
    public class AccountModel
    {
        public string Id { get; set; }
        public int AccountLevel { get; set; }
        public string ClassificationId { get; set; }
        public string Type { get; set; }
        [Required(ErrorMessage = "Account Name is required")]
        public string AccountName { get; set; }
        public decimal OpeningBalance { get; set; }
        public int AccountNumber { get; set; }
        public string ParentId { get; set; }
        public bool IsHeader { get; set; }
        public int HierarchyLevel { get; set; }
        public int OrderBy { get; set; }
        public int ClassificationNumber { get; set; }
        public DateTime CreatedOnUtc{ get; set; }
        public DateTime UpdateOnUtc { get; set; }
        public ClassificationModel Classifications { get; set; }
    }
}
