using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerApp
{
    public class Role
    {
        public int AccountId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public ClientSocket ClientSocket { get; set; }
    }
}
