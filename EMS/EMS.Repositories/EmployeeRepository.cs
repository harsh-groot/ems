using AutoMapper;
using EMS.DataModel;
using EMS.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EMS.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private EMSDataContext _context;

        public EmployeeRepository(EMSDataContext context, IMapper mapper)
        {
            _context = context;
        }

        public async Task<List<Employees>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employees> GetById(int id)
        {
            var employee = await GetEmployee(id);

            if (employee == null) throw new KeyNotFoundException("Employee not found");

            return employee;
        }

        public async Task Create(Employees model)
        {
            // validate
            if (_context.Employees.Any(x => x.Email.ToLower() == model.Email.ToLower()))
                throw new Exception("Employee with the email '" + model.Email + "' already exists");

            // save emplyoee
            await _context.Employees.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Employees model)
        {
            var employee = await GetEmployee(id);

            // validate
            if (model.Email.ToLower() != employee.Email.ToLower() && _context.Employees.Any(x => x.Email.ToLower() == model.Email.ToLower()))
                throw new Exception("Employee with the email '" + model.Email + "' already exists");

            _context.Employees.Update(model);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var employee = await GetEmployee(id);

                if (employee == null) throw new Exception("Employee not found.");

                _context.Employees.Remove(employee);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<Employees> GetEmployee(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(d => d.Id == id);
            if (employee == null) throw new KeyNotFoundException("Employee not found");
            return employee;
        }
    }
}