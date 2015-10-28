using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DropIt.Data.Interfaces.Users;

namespace DropIt.Data.Interfaces.Services
{
    public interface IAccountService
    {
        void StoreCredentials(IUser toStore);
        IUser LoadCredentials(IUser destination);
        void ClearCredentials();
    }
}
