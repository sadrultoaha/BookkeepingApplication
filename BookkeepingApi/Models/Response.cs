using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookkeepingApi.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }

        public Response(int statusCode, bool status, string message)
        {
            this.StatusCode = statusCode;
            this.Status = status;
            this.Message = message;
        }


    }
}
