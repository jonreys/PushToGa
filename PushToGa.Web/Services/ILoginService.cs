using PushToGa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushToGa.Web.Services
{
    public interface ILoginService
    {
        Task<UserModel> LoginAsync(string username, string password);
    }
}
