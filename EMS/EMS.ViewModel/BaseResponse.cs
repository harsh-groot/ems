using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel
{
    public class BaseResponse
    {
        public ResponseCodes ResponseCodes { get; set; }

        public string ResponseMessage { get; set; }
    }

    public enum ResponseCodes
    {
        OK = 200,
        BadRequest = 400,
        Unauthorized = 401,
        NotFound = 404,
        InternalServerError = 500,
    }
}
