using EMS.DataModel;
using EMS.ViewModel;

namespace EMS.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employees>> GetAll();

        Task<Employees> GetById(int id);

        Task Create(Employees model);

        Task Update(int id, Employees model);

        Task<bool> Delete(int id);
    }
}
