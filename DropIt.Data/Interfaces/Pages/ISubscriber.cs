using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropIt.Data.Interfaces.Pages
{
    public interface ISubscriber
    {
        void Subscribe();
        void Unsubscribe();
    }
}
