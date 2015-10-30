using System;
using System.Diagnostics.Contracts;

namespace DropIt.Data.Interfaces.Users
{
    /// <summary>
    /// Represents both a system user as well as an Auth0 user.
    /// </summary>
	public interface IUser
	{
        /// <summary>
        /// The token used to fetch a new IdToken after it has expired.
        /// </summary>
        string RefreshToken { get; set; } 

        /// <summary>
        /// The JWT IdToken that contains profile information about the user.
        /// </summary>
        string IdToken { get; set; }

        /// <summary>
        /// The email of the user as pulled from the IdToken.
        /// </summary>
        string Email { get; set; }
	}
}

