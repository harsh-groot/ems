using AutoMapper;
using Castle.Core.Configuration;
using EMS.DataModel;
using EMS.Repositories;
using EMS.Services;
using EMS.Tests.Common;
using EMS.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Shouldly;
using System.Reflection;
using System.Text.Json;

namespace EMS.Tests
{
    public class EMSManagerTestsSut : CommonSutBase
    {
        public List<Employees> Employees { get; set; } = new();

        protected override IHostBuilder CreateSutBuilder()
        {
            var builder = base.CreateSutBuilder();

            return builder.ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;

                services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfiles)))
                    .AddScoped<IEmployeeService, EmployeeService>()
                    .AddScoped<IEmployeeRepository, MockEmployeeRepository>();
            });
        }

        private static IEmployeeRepository GetE()
        {
            var mockMapper = new Mock<IMapper>();
            return new Mock<EmployeeRepository>(GetInMemoryMockDB(), mockMapper.Object).Object;
        }
        private static EMSDataContext GetInMemoryMockDB()
        {
            // Set Up Entity InMemory Database

            var config = new ConfigurationBuilder()

            .AddEnvironmentVariables();

            var configuration = config.Build();

            var dbContext = new EMSDataContext(configuration);
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }

    public class EMSTest : CommonTestBase<EMSManagerTestsSut>
    {
        [Fact]
        public async Task GetAllEmployees_Returns_Success()
        {
            var configuration = new ConfigurationBuilder()
               .AddInMemoryCollection()
               .Build();

            await StartSutAsync(configuration);
            Services.ShouldNotBeNull();

            var requestModel = CommonActionSut.EmsRequestModel();
            requestModel.ShouldNotBeNull();

            var service = Services?.GetRequiredService<IEmployeeService>();
            service.ShouldNotBeNull();

            if (Services.GetRequiredService<IEmployeeRepository>() is not MockEmployeeRepository dbManager)
                throw new Exception(nameof(dbManager));

            var serviceResponse = await service.GetAll();
            serviceResponse.ShouldNotBeNull();
            Assert.Equal(3, dbManager.Database.Count);
        }

        [Fact]
        public async Task GetEmployeeById_Returns_Success()
        {
            var configuration = new ConfigurationBuilder()
               .AddInMemoryCollection()
               .Build();

            await StartSutAsync(configuration);
            Services.ShouldNotBeNull();

            var requestModel = CommonActionSut.EmsRequestModel();
            requestModel.ShouldNotBeNull();

            var service = Services?.GetRequiredService<IEmployeeService>();
            service.ShouldNotBeNull();

            if (Services.GetRequiredService<IEmployeeRepository>() is not MockEmployeeRepository dbManager)
                throw new Exception(nameof(dbManager));

            var serviceResponse = await service.GetById(1);
            serviceResponse.ShouldNotBeNull();
            Assert.NotNull(serviceResponse.Name);
        }

        [Fact]
        public async Task CreateEmployee_Returns_Success()
        {
            var configuration = new ConfigurationBuilder()
               .AddInMemoryCollection()
               .Build();

            await StartSutAsync(configuration);
            Services.ShouldNotBeNull();

            var requestModel = CommonActionSut.EmsRequestModel();
            requestModel.ShouldNotBeNull();

            var service = Services?.GetRequiredService<IEmployeeService>();
            service.ShouldNotBeNull();

            if (Services.GetRequiredService<IEmployeeRepository>() is not MockEmployeeRepository dbManager)
                throw new Exception(nameof(dbManager));

            requestModel.Id = 0;
            var serviceResponse = await service.Upsert(requestModel);
            serviceResponse.ShouldBe(true);
            Assert.Equal(4, dbManager.Database.Count);
            Assert.True(serviceResponse);
        }

        [Fact]
        public async Task UpdateEmployee_Returns_Success()
        {
            var configuration = new ConfigurationBuilder()
               .AddInMemoryCollection()
               .Build();

            await StartSutAsync(configuration);
            Services.ShouldNotBeNull();

            var requestModel = CommonActionSut.EmsRequestModel();
            requestModel.ShouldNotBeNull();

            var service = Services?.GetRequiredService<IEmployeeService>();
            service.ShouldNotBeNull();

            if (Services.GetRequiredService<IEmployeeRepository>() is not MockEmployeeRepository dbManager)
                throw new Exception(nameof(dbManager));
            
            requestModel.Name = "UpdatedName";
            requestModel.Id = 1;
            var serviceResponse = await service.Upsert(requestModel);
            serviceResponse.ShouldBe(true);

            var updatedRecord = dbManager.Database.FirstOrDefault(d => d.Id == 1)!;

            Assert.Equal("UpdatedName", updatedRecord.Name);
        }

        [Fact]
        public async Task DeleteEmployee_Returns_Success()
        {
            var configuration = new ConfigurationBuilder()
               .AddInMemoryCollection()
               .Build();

            await StartSutAsync(configuration);
            Services.ShouldNotBeNull();

            var requestModel = CommonActionSut.EmsRequestModel();
            requestModel.ShouldNotBeNull();

            var service = Services?.GetRequiredService<IEmployeeService>();
            service.ShouldNotBeNull();

            if (Services.GetRequiredService<IEmployeeRepository>() is not MockEmployeeRepository dbManager)
                throw new Exception(nameof(dbManager));

            var serviceResponse = await service.Delete(1);
            serviceResponse.ShouldBe(true);
            Assert.Equal(2, dbManager.Database.Count);
        }

    }
}