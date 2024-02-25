using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class PermissionsEmployee
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        [ForeignKey("PermissionTypeId")]
        public int PermissionTypeId { get; set; }
        public virtual List<Employee>? Employees { get; set; }
        public virtual PermissionType? PermissionTypes { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
