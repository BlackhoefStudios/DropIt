using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DropIt.Data.Interfaces.Users;

namespace DropIt.Data.Interfaces.Services
{
    /// <summary>
    /// An interface for working with storing and retrieving account credentials.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Used to store credentials on a device/platform.
        /// </summary>
        /// <param name="toStore">The credentials you are storing.</param>
        void StoreCredentials(IUser toStore);

        /// <summary>
        /// Loads credentials from device storage.
        /// </summary>
        /// <param name="destination">An object to populate values for. This object will get filled and then returned.</param>
        /// <returns>The same object that was passed in, only now the properties are filled.</returns>
        IUser LoadCredentials(IUser destination);

        /// <summary>
        /// Removes all credential data from the device storage.
        /// </summary>
        void ClearCredentials();
    }
}
