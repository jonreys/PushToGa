using PushToGa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushToGa.Web.Services
{
    public class LoginService : ILoginService
    {
        public async Task<UserOutput> LoginAsync(string username, string password)
        {
            var result = new UserOutput();
            try
            {
                /// Create a Fake Login Procedure
                if (username == "admin")
                {
                    if (password == "Pa$$w0rd")
                    {
                        Random rnd = new Random();
                        int card = rnd.Next(1000);     // creates a number between 0 and 1000
                        result.Id = card;
                        result.Fullname = "Jonathan Ramos Reyes";
                        result.EmailAddress = "jonathan_reyes@data3.com.au";
                        result.Token = Guid.NewGuid().ToString();
                        result.Username = username;                        
                        result.Roles.Add("Administrator");
                    }
                }
                else
                    result = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            await Task.CompletedTask;

            return result;
        }
    }
}
