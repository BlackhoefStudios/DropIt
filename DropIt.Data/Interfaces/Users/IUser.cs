using System;
using System.Diagnostics.Contracts;

namespace DropIt.Data.Interfaces.Users
{
	public interface IUser
	{
        string RefreshToken { get; set; } 
        string IdToken { get; set; }
        string Email { get; set; }
	}
}

