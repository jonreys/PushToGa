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
        public string Fullname { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsEmailConfirmed { get; set; } = false;
        public bool? IsOperator { get; set; } = false;
        public int RoleId { get; set; } = 1;
        public List<string> Roles { get; set; }
        public CompanyModel Company { get; set; }
        public string Token { get; set; }
        public string Status { get; set; } = "Inactive";
    }
}
