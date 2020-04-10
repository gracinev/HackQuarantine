using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackQuarantine.Models
{
    public class PersonModel
    {
        public List<RoleModel> Roles { get; set; }
        public string Name { get; set; }
    }

    public class RoleModel
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
