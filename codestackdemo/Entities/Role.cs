using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace codestackdemo.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}
