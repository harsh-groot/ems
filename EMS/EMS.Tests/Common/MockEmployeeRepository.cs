using AutoFixture;
using EMS.DataModel;
using EMS.Repositories;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.Common
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private const int NumberOfRecords = 3;
        public List<Employees> Database { get; set; } = GetEMSEmployees();
        public async Task Create(Employees model)
        {
            Database.Add(model);
            await Task.CompletedTask;
        }

        public async Task<bool> Delete(int id)
        {
            Database.RemoveAt(id);
            return await Task.FromResult(true);
        }

        public async Task<List<Employees>> GetAll()
        {

            return await Task.FromResult(Database);
        }

        public async Task<Employees> GetById(int id)
        {
            return await Task.FromResult(Database.FirstOrDefault(o => o.Id == id)!);
        }

        public async Task Update(int id, Employees model)
        {
            var employee = Database.FirstOrDefault(o => o.Id == id)!;
            employee.DateOfBirth = model.DateOfBirth;
            employee.Name = model.Name;
            employee.Email = model.Email;
            employee.Department = model.Department;
            Database[id] = employee;
            await Task.CompletedTask;
        }
        private static List<Employees> GetEMSEmployees()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var model = fixture.CreateMany<Employees>(NumberOfRecords);
            model.ToList()[0].Id = 1;
            return model.ToList();
        }
    }
}
