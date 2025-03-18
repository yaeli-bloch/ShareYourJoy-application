using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Password { get; set; }        
        public List<GroupUser> Users { get; set; } 
        public int AdminId { get; set; }
        public string AdminPassword { get; set; }
    }
}
