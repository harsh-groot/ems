using EMS.DataModel;
using EMS.ViewModel;

namespace EMS.Services
{
    public interface IEmployeeService
    {
        Task<List<Employees>> GetAll();
        
        Task<Employees> GetById(int id);
        
        Task<bool> Upsert(EMSRequest model);

        Task<bool> Delete(int id);
    }
}
