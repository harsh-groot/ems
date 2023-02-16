using AutoMapper;
using EMS.DataModel;
using EMS.Repositories;
using EMS.ViewModel;

namespace EMS.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<List<Employees>> GetAll()
        {
            return await _employeeRepository.GetAll();
        }

        public async Task<Employees> GetById(int id)
        {
            return await _employeeRepository.GetById(id);
        }

        public async Task<bool> Upsert(EMSRequest model)
        {
            try
            {
                var employee = new Employees();
                if (model.Id > 0)
                {
                    employee = await _employeeRepository.GetById(model.Id);
                    _mapper.Map(model, employee);
                }
                else
                {
                    employee = _mapper.Map<Employees>(model);
                }

                await _employeeRepository.Upsert(employee);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _employeeRepository.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
