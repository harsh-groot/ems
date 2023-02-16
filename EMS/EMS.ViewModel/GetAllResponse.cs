using EMS.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel
{
    public class GetAllResponse : BaseResponse
    {
        public List<Employees> ResponseData { get; set; }
    }

    public class GetSingleResponse : BaseResponse
    {
        public Employees ResponseData { get; set; }
    }
}
