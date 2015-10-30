using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropIt.Data.Interfaces.Pages
{
    /// <summary>
    /// Represents an object that sends and recieves messages using the Xamarin.Forms.MessagingCenter.
    /// </summary>
    public interface ISubscriber
    {
        /// <summary>
        /// A method that is meant to handle subscribing to messages. 
        /// This should only be called once.
        /// </summary>
        void Subscribe();

        /// <summary>
        /// A method that is used to handle unsubscribing from events that were subscribed too in Subscribe.
        /// </summary>
        void Unsubscribe();
    }
}
