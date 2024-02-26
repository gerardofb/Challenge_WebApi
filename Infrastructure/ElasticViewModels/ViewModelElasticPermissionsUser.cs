using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.ElasticViewModels
{
    public class ViewModelElasticPermissionsUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public Guid PermissionGuid { get; set; }
        public string PermissionName { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
