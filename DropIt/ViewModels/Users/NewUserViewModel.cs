using System;
using BlackhoefStudios.Common.ViewModels.Bases;
using System.Windows.Input;
using Xamarin.Forms;

namespace DropIt.ViewModels.Users
{
	public class NewUserViewModel : ObservableViewModel
	{
		string email;
		public string Email {
			get { return email; }
			set {
				email = value;
				OnPropertyChanged ("Email");
			}
		}

		public ICommand SendEmail {
			get;
			protected set;
		}

		public NewUserViewModel (INavigation nav)
		{
			SendEmail = new Command (async () => {
				if(String.IsNullOrEmpty(Email)) return;
				//send email
				await nav.PopAsync();
			});
		}
	}
}

