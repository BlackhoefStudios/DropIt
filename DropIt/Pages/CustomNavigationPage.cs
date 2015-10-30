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
    /// <summary>
    /// Provides a base class for customizable navigation pages.
    /// </summary>
    public class CustomNavigationPage : NavigationPage
    {
        /// <summary>
        /// Creates a new navigation page for pushing and poping views.
        /// </summary>
        /// <param name="content">The page content to set as the initial content to view.</param>
        public CustomNavigationPage(Page content) : base(content)
        {
            //iOS needs extra padding up top to handle the status bar.
            if(Device.OS == TargetPlatform.iOS)
            {
                //add padding to account for the top bar
                var originalPadding = content.Padding;

                //padding needed if the top is less than 20
                if(originalPadding.Top < 20)
                {
                    content.Padding = new Thickness(originalPadding.Left, 20,
                        originalPadding.Right, originalPadding.Bottom);
                }
            }

            Title = content.Title;

            //wire up two events to fire when a page is added or removed.
            Pushed += CustomNavigationPage_Pushed;
            Popped += CustomNavigationPage_Popped;

            //trigger the initial check for subscribing or not.
            HandleEvent(content, true);
        }

        
        /// <summary>
        /// Determines if a page needs to fire the Subscribe or Unsubscribe event when pushed or poped.
        /// This is important so there are no duplicate messagings being recieved or deleted.
        /// </summary>
        /// <param name="page">The page to check.</param>
        /// <param name="shouldSubscribe">if true, subscribe if the page can. Otherwise, unsubscribe.</param>
        void HandleEvent(Page page, bool shouldSubscribe)
        {
            //check if the pages view model supports our subscription based model.
            var binding = page.BindingContext as ISubscriber;
            if (binding != null)
            {
                //it does, so subscribe or unsubscribe accordingly.
                if (shouldSubscribe)
                    binding.Subscribe();
                else
                    binding.Unsubscribe();
            }
        }

        private void CustomNavigationPage_Popped(object sender, NavigationEventArgs e)
        {
            //page removed from view, so check if we need to unsubscribe.
            HandleEvent(e.Page, false);
        }

        private void CustomNavigationPage_Pushed(object sender, NavigationEventArgs e)
        {
            //page added to view, so check if we need to subscribe.
            HandleEvent(e.Page, true);
        }
    }
}
