using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.Web.Models
{
    public class LoginDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool RemeberMe { get; set; }
    }
}
