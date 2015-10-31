using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DropIt.Data.Interfaces.Services;
using DropIt.Data.Interfaces.Users;
using DropIt.Data.Services;
using Xamarin.Forms;
using BlackhoefStudios.Common.Interfaces.Services;

namespace DropIt.Data.Services
{
    /// <summary>
    /// A helper for storing and retrieving user credentials.
    /// </summary>
    public sealed class AccountHelper : IAccountService
    {
        private readonly ISecureStorage storage;
        const string IdTokenKey = "IdToken";
        const string EmailKey = "Email";
        const string RefreshKey = "RefreshToken";

        /// <summary>
        /// Create a new instance of the AccountHelper.
        /// </summary>
        /// <param name="storage">The secure storage implemenation to use for fetching and setting values securely.</param>
        public AccountHelper(ISecureStorage storage)
        {
            this.storage = storage;
        }

        /// <summary>
        /// Stores the specified user into the devices secure storage.
        /// </summary>
        /// <param name="toStore">The user credentials to store.</param>
        public void StoreCredentials(IUser toStore)
        {
            //Using a Key-Value storage pattern, store the strings as bytes with the given key.
            storage.Store(RefreshKey, Encoding.UTF8.GetBytes(toStore.RefreshToken));
            storage.Store(IdTokenKey, Encoding.UTF8.GetBytes(toStore.IdToken));
            storage.Store(EmailKey, Encoding.UTF8.GetBytes(toStore.Email));
        }

        /// <summary>
        /// Removes all the stored user credentials from secured storage.
        /// </summary>
        public void ClearCredentials()
        {
            //delete each key from storage.
            storage.Delete(RefreshKey);
            storage.Delete(IdTokenKey);
            storage.Delete(EmailKey);
        }

        /// <summary>
        /// Loads the credentials from secure storage.
        /// </summary>
        /// <param name="destinationUser">The object to fill the values for.</param>
        /// <returns>The same object as the input, except with populated values.
        ///  Or it returns null if the user doesn't exist in storage.</returns>
        public IUser LoadCredentials(IUser destinationUser)
        {
            // make sure our base object has a value so we know how to populate it.
            if(destinationUser == null)
                throw new NullReferenceException("Destination must not be null.");

            try
            {
                //get the values from storage
                var email = storage.Retrieve(EmailKey);
                var idToken = storage.Retrieve(IdTokenKey);
                var refreshToken = storage.Retrieve(RefreshKey);

                //now get their string values and assign to the user.
                destinationUser.Email = Encoding.UTF8.GetString(email, 0, email.Length);
                destinationUser.IdToken = Encoding.UTF8.GetString(idToken, 0, idToken.Length);
                destinationUser.RefreshToken = Encoding.UTF8.GetString(refreshToken, 0 , refreshToken.Length);

                //return the user with populated values.
                return destinationUser;
            }
            catch (Exception e)
            {
                //let it fail silently. This way we can check if the returned credentials is null or not.
            }
            return null;
        }
    }
}
