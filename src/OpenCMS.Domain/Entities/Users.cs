using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Domain.Entities
{
    public class Users:BaseEntity<string>
    {
        public Users()
        {
            this.Roles = new HashSet<Roles>();
        }

        [Key]
        [MaxLength(128)]
        public string Id { get; set; }
        [MaxLength(128)]
        public string FirstName { get; set; }
        [MaxLength(128)]
        public string MiddleName { get; set; }
        [MaxLength(128)]
        public string LastName { get; set; }
        [MaxLength(128)]
        public string UserName { get; set; }
        [MaxLength(128)]
        public string Email { get; set; }
        [MaxLength(500)]
        public string PasswordHash { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? PhoneConfirmed { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Roles> Roles { get; set; }
    }
}
