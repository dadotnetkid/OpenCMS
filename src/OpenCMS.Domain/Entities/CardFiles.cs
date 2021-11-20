using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Domain.Entities
{
    [Table("CardFiles")]
    public class CardFiles:BaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(128)]
        public string FirstName { get; set; }
        [MaxLength(128)]
        public string MiddleName { get; set; }
        [MaxLength(128)]
        public string LastName { get; set; }
        [MaxLength(128)]
        public string AddressLine1 { get; set; }
        [MaxLength(128)]
        public string AddressLine2 { get; set; }
        [MaxLength(128)]
        public string CreatedBy { get; set; }
        [MaxLength(128)]
        public string Email { get; set; }
        [MaxLength(128)]
        public string PhoneNumber { get; set; }
        public int CardFileType { get; set; }
        [ForeignKey("CreatedBy")]
        public Users CreatedByUser { get; set; }
    }

}
