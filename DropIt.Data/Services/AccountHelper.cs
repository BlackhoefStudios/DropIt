using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DropIt.Data.Interfaces.Services;
using DropIt.Data.Interfaces.Users;
using DropIt.Data.Services;
using Xamarin.Forms;

namespace DropIt.Data.Services
{
    public sealed class AccountHelper : IAccountService
    {
        private readonly ISecureStorage storage;
        public AccountHelper(ISecureStorage storage)
        {
            this.storage = storage;
        }

        public void StoreCredentials(IUser toStore)
        {
            storage.Store("RefreshToken", Encoding.UTF8.GetBytes(toStore.RefreshToken));
            storage.Store("IdToken", Encoding.UTF8.GetBytes(toStore.IdToken));
            storage.Store("Email", Encoding.UTF8.GetBytes(toStore.Email));
        }

        public void ClearCredentials()
        {
            storage.Delete("RefreshToken");
            storage.Delete("IdToken");
            storage.Delete("Email");
        }

        public IUser LoadCredentials(IUser destinationUser)
        {
            if(destinationUser == null)
                throw new NullReferenceException("Destination must not be null.");

            try
            {
                var email = storage.Retrieve("Email");
                var idToken = storage.Retrieve("IdToken");
                var refreshToken = storage.Retrieve("RefreshToken");

                destinationUser.Email = Encoding.UTF8.GetString(email, 0, email.Length);
                destinationUser.IdToken = Encoding.UTF8.GetString(idToken, 0, idToken.Length);
                destinationUser.RefreshToken = Encoding.UTF8.GetString(refreshToken, 0 , refreshToken.Length);
                return destinationUser;
            }
            catch (Exception e)
            {
            }
            return null;
        }
    }
}
