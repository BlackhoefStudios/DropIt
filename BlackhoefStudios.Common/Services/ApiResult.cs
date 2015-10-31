using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackhoefStudios.Common.Services
{
    public class ApiResult<T> : ApiResult where T : class
    {
        public T Data { get; set; }
    }

    public class ApiResult
    {
        public string Message { get; set; }
        public int ResponseCode { get; set; }
    }
}
