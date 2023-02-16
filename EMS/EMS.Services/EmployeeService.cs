using AutoMapper;
using EMS.DataModel;
using EMS.Repositories;
using EMS.ViewModel;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> Create(EMSRequest model)
        {
            try
            {
                var employee = _mapper.Map<Employees>(model);

                await _employeeRepository.Create(employee);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(int id, EMSRequest model)
        {
            try
            {
                var employee = _mapper.Map<Employees>(model);

                await _employeeRepository.Update(id, employee);
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
