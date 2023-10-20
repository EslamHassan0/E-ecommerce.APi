using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerceAssignment.EF.Helpers
{
    public class AuthModel
    {
        public string Massage { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<String> Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime LastLoginTime { get; set; }

    }
}
