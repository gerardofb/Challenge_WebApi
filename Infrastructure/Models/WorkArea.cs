using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class WorkArea
    { 
        public int Id { get; set; }
        public string? AreaName { get; set; }
        public Employee? Employee { get; set; }
    }
}
