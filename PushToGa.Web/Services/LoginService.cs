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
                    if (password == "1234")
                    {
                        result.Id = 1045;
                        result.Username = username;
                        result.Status = "Active";
                        result.IsInternalUser = true;
                    }
                    else
                        result.Status = "NoMatch";
                }
                else
                {                    
                    /// Second User
                    if (username == "Support")
                    {
                        if (password == "1234")
                        {
                            result.Id = 1189;
                            result.Username = username;
                            result.Status = "Active";
                            result.IsInternalUser = false;

                        }
                        else
                            result.Status = "NoMatch";
                    }
                    else
                        result.Status = "NoUser";
                }
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
