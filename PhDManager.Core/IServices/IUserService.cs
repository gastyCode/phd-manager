using PhDManager.Core.Models;
using PhDManager.Core.ValidationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhDManager.Core.IServices
{
    public interface IUserService
    {
        Task<bool> Login(UserLogin userLogin);
    }
}
