using EMS.DataModel;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Services
{
    public interface IEmployeeService
    {
        Task<List<Employees>> GetAll();
        
        Task<Employees> GetById(int id);
        
        Task<bool> Create(EMSRequest model);

        Task<bool> Update(int id, EMSRequest model);

        Task<bool> Delete(int id);
    }
}
