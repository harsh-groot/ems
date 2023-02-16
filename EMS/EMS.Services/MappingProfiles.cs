using AutoMapper;
using EMS.DataModel;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Services
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EMSRequest, Employees>().ReverseMap();
        }
    }
}
