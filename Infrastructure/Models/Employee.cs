using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Employee
    {
      
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime LastUpdated { get; set; }
        [ForeignKey("WorkArea")]
        public int WorkAreaId { get; set; }
        public virtual WorkArea? WorkArea { get; set; }
        public virtual PermissionsEmployee? PermissionEmployees { get; set; }
    }
}
