using System;
using Xamarin.Forms;

namespace BlackhoefStudios.Common.Interfaces.Pages
{
    /// <summary>
    /// Represents a Xamarin.Forms application that is running.
    /// </summary>
	public interface IApplication
	{
        /// <summary>
        /// The main page for the application.
        /// </summary>
		Page MainPage { get; }
	} 
}

