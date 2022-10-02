using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Responses
{
    public class Response<T>
    {
        public Response(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
