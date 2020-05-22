using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Service.IService
{
    public interface IAuthenticationService
    {
        Task<User> Authenticate(string username, string password);

    }
}
