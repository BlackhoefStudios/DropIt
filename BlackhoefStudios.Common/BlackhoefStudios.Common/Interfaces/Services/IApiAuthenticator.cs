using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackhoefStudios.Common.Interfaces.Services
{
    public interface IApiAuthenticator
    {
        Task<string> GetToken();
    }
}
