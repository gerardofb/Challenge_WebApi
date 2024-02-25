using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class MaterializedViewPermissions
    {
        public string UserName { get; set; }
        public string LastName { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }

    }
}
