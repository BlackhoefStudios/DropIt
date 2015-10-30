using System;
using DropIt.Data.Interfaces.Services;
using System.Threading.Tasks;
using DropIt.Data.Interfaces.Users;
using DropIt.Pages;
using Xamarin.Forms;

namespace DropIt.Services
{
    /// <summary>
    /// Provides access to the currently logged in user to the system.
    /// </summary>
	public static class LoginService
	{
        /// <summary>
        /// The currently logged in user.
        /// </summary>
	    public static IUser CurrentUser { get; set; }
	}
}

