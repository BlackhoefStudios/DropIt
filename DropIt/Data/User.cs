using System;
using DropIt.Data.Interfaces.Users;

namespace DropIt.Data
{
	public class User : IUser
	{
		public User ()
		{
		}

	    public string RefreshToken { get; set; }
	    public string IdToken { get; set; }
	    public string Email { get; set; }
    }
}

