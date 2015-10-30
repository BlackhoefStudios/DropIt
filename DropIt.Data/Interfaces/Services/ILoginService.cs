using System;
using System.Threading.Tasks;
using DropIt.Data.Interfaces.Users;

namespace DropIt.Data.Interfaces.Services
{
    /// <summary>
    /// An interface for handling login. This is mostly an interop for Auth0 platfrom specific code.
    /// </summary>
	public interface ILoginService
	{
        /// <summary>
        /// Presents the user with the necessary view to login to the application.
        /// </summary>
        /// <returns>A Task that represents the asynchronous call for the view.</returns>
		Task Login();

        /// <summary>
        /// Sets the currently logged in user for the application.
        /// </summary>
        /// <param name="loggedInUser">The signed in user.</param>
	    void SetUser(IUser loggedInUser);
	}
}

