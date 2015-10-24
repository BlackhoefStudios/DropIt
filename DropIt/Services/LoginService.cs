using System;
using DropIt.Data.Interfaces.Services;
using System.Threading.Tasks;
using DropIt.Data.Interfaces.Users;
using DropIt.Pages;
using Xamarin.Forms;

namespace DropIt.Services
{
	public class LoginService
	{
	    public static IUser CurrentUser { get; set; }
	}
}

