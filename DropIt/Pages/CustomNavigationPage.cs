using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DropIt.Data.Interfaces.Pages;
using System.Diagnostics;

namespace DropIt.Pages
{
    public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage(Page content) : base(content)
        {
            if(Device.OS == TargetPlatform.iOS)
            {
                //add padding to account for the top bar
                var originalPadding = content.Padding;
                if(originalPadding.Top < 20)
                {
                    content.Padding = new Thickness(originalPadding.Left, 20,
                        originalPadding.Right, originalPadding.Bottom);
                }
            }
            SetHasNavigationBar(content, false);
            Pushed += CustomNavigationPage_Pushed;
            Popped += CustomNavigationPage_Popped;
            HandleEvent(content, true);
        }

        

        void HandleEvent(Page page, bool shouldSubscribe)
        {
            var binding = page.BindingContext as ISubscriber;
            if (binding != null)
            {
                if (shouldSubscribe)
                    binding.Subscribe();
                else
                    binding.Unsubscribe();
            }
        }

        private void CustomNavigationPage_Popped(object sender, NavigationEventArgs e)
        {
            HandleEvent(e.Page, false);
        }

        private void CustomNavigationPage_Pushed(object sender, NavigationEventArgs e)
        {
            HandleEvent(e.Page, true);
        }
    }
}
