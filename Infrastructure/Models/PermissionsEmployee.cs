using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class PermissionsEmployee
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public virtual IEnumerable<PermissionsEmployee>? Employees { get; set; }
        public virtual IEnumerable<PermissionType>? PermissionTypes { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
