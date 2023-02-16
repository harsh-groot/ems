using AutoFixture;
using EMS.DataModel;
using EMS.Repositories;
using EMS.Services;
using EMS.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;

namespace EMS.Tests.Common
{

    public class CommonActionSut : CommonSutBase
    {
        public static List<Employees> GetEMSEmployees()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var model = fixture.CreateMany<Employees>(3);
            return model.ToList();
        }

        public static EMSRequest EmsRequestModel()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var model = fixture.Create<EMSRequest>();
            return model;
        }

        public static IConfigurationRoot GetConfigurationRoot()
        {
            var appSettings = new Dictionary<string, string>();

            return new ConfigurationBuilder()
                .AddInMemoryCollection(appSettings!)
                .Build();
        }
        protected override IHostBuilder CreateSutBuilder()
        {
            var builder = base.CreateSutBuilder();


            return builder.ConfigureServices((_, services) =>
            {
                services.AddScoped<IEmployeeService, EmployeeService>();
                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            });
        }

    }
}
