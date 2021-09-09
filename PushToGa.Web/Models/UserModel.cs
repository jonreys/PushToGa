using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushToGa.Web.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool? IsInternalUser { get; set; } = false;
        public CompanyModel Company { get; set; }
        public string Status { get; set; } = "Inactive";
    }
}
