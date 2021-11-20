using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenCMS.Web.Infrastructure.Models
{
    public class PermissionsInRolesModel
    {
        

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("permissionId")]
        public int PermissionId { get; set; }

        [JsonPropertyName("permission")]
        public string Permission { get; set; }

        [JsonPropertyName("roleId")]
        public string RoleId { get; set; }
        [JsonPropertyName("canView")]
        public bool CanView { get; set; }
        [JsonPropertyName("canAdd")]
        public bool CanAdd { get; set; }

        [JsonPropertyName("canUpdate")]
        public bool CanUpdate { get; set; }

        [JsonPropertyName("canDelete")]
        public bool CanDelete { get; set; }
    }
}
