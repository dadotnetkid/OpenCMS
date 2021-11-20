using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Domain.Entities
{
    [Table("Permissions")]
    public class Permissions:BaseEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public string Permission { get; set; }
    }
    [Table("PermissionsInRoles")]
    public class PermissionsInRoles : BaseEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public string RoleId { get; set; }
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public Permissions Permission { get; set; }
    }
}
