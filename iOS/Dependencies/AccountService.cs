using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DropIt.Data;
using DropIt.Data.Interfaces.Services;
using DropIt.Data.Interfaces.Users;
using DropIt.iOS.Dependencies;
using DropIt.Services;

[assembly: Xamarin.Forms.Dependency(typeof(AccountService))]
namespace DropIt.iOS.Dependencies
{
    public sealed class AccountService : SecureStorage, IAccountService
    {
        public void StoreCredentials(IUser toStore)
        {
            Store("RefreshToken", Encoding.UTF8.GetBytes(toStore.RefreshToken));
            Store("IdToken", Encoding.UTF8.GetBytes(toStore.IdToken));
            Store("Email", Encoding.UTF8.GetBytes(toStore.Email));
        }

        public void ClearCredentials()
        {
            Delete("RefreshToken");
            Delete("IdToken");
            Delete("Email");
        }

        public IUser LoadCredentials()
        {
            try
            {
                var user = new User()
                {
                    Email = Encoding.UTF8.GetString(Retrieve("Email")),
                    IdToken = Encoding.UTF8.GetString(Retrieve("IdToken")),
                    RefreshToken = Encoding.UTF8.GetString(Retrieve("RefreshToken"))
                };
                return user;
            }
            catch(Exception e)
            {
            }
            return null;
        }
    }
}
