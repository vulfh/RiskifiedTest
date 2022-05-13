using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardConnectors
{
    public class Response
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public Response(bool success,string message)
        {
            Success = success;
            Message = message;
        }
    }
}
