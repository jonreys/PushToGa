using PushToGa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushToGa.Web.Services
{
    public class LoginService : ILoginService
    {
        public async Task<UserModel> LoginAsync(string username, string password)
        {
            var result = new UserModel();
            try
            {
                /// Create a Fake Login Procedure
                if (username == "Admin")
                {
                    if (password == "Pa$$w0rd")
                    {
                        result.Id = 1045;
                        result.Fullname = "Jonathan Ramos Reyes";
                        result.EmailAddress = "jonathan_reyes@data3.com.au";
                        result.Token = Guid.NewGuid().ToString();
                        result.Username = username;
                        result.Status = "Active";
                        result.Roles = new List<string> 
                        {
                            "Administrator"
                        };                        
                        result.Company = new CompanyModel
                        {
                            CompanyId = 560,
                            CompanyName = "ABC WLL"
                        };                        
                    }
                    else
                        result.Status = "NoMatch";
                }
                else
                    result.Status = "NoUser";
            }
            catch (Exception ex)
            {
                result.Status = "APIError";
                throw new Exception(ex.Message);
            }
            await Task.CompletedTask;

            return result;
        }
    }
}
