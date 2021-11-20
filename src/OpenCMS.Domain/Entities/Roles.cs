using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Domain.Entities
{
    [Table("Roles")]
    public partial class Roles : BaseEntity<string>
    {
        public Roles()
        {
            this.Users = new HashSet<Users>();
            this.PermissionsInRoles = new HashSet<PermissionsInRoles>();
        }

        [Key]
        [MaxLength(128)]
        public string Id { get; set; }
        [MaxLength(128)]
        public string Role { get; set; }

        public virtual ICollection<Users> Users { get; set; }
        public virtual ICollection<PermissionsInRoles> PermissionsInRoles { get; set; }
    }
}
