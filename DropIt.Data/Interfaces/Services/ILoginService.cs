using System;
using System.Threading.Tasks;
using DropIt.Data.Interfaces.Users;

namespace DropIt.Data.Interfaces.Services
{
	public interface ILoginService
	{
		Task Login();
	    void SetUser(IUser loggedInUser);
	}
}

