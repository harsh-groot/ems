using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EMS.Tests.Common
{
    public abstract class CommonSutBase : IDisposable
    {
        #region SUT Name

        /// <summary>
        /// Get the SUT Name. Returns the test class by default
        /// </summary>
        /// <returns>Test system name</returns>
        /// <remarks>
        /// The SUT Name is used to create resources, like databases, that are specific to the test.
        /// </remarks>
        public virtual string GetSUTName() => GetType().Name;

        #endregion

        #region Temp Data Directory

        private string _tempDataDirectory = string.Empty;

        protected void CreateTempDataDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), $"{GetSUTName()}_{Path.GetRandomFileName()}");
            Directory.CreateDirectory(tempDirectory);
            _tempDataDirectory = tempDirectory;
        }

        protected void DeleteTempDataDirectory()
        {
            if (!string.IsNullOrEmpty(_tempDataDirectory))
            {
                Directory.Delete(_tempDataDirectory, true);
            }
        }

        protected string GetTempDataDirectory()
        {
            return _tempDataDirectory;
        }

        #endregion

        #region Configure

        /// <summary>
        /// Override where you configure the SUT, selecting any required test resources
        /// </summary>
        protected virtual void Configure()
        {

        }

        #endregion

        #region Host

        /// <summary>
        /// Create the SUT Builder 
        /// </summary>
        /// <returns>App Host Builder</returns>
        protected virtual IHostBuilder CreateSutBuilder()
        {
            CreateTempDataDirectory();
            return Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder();
        }

        /// <summary>
        /// Set the SUT Running
        /// </summary>
        /// <returns>Task</returns>
        public virtual Task StartSUTAsync(IConfiguration? configuration = null)
        {
            Configure();
            CreateTempDataDirectory();

            var hostBuilder = CreateSutBuilder();
            if (configuration != null)
            {
                hostBuilder.ConfigureAppConfiguration(builder => builder.AddConfiguration(configuration));
            }
            Host = hostBuilder.Build();

            return Host.StartAsync();
        }

        /// <summary>
        /// Ensure that the SUT is started
        /// </summary>
        /// <returns></returns>
        public Task EnsureSUTStarted()
        {
            if (Host != null)
                return Task.CompletedTask;

            return StartSUTAsync();
        }

        /// <summary>
        /// Stop the SUT
        /// </summary>
        /// <returns>Task</returns>
        public async virtual Task StopSUTAsync()
        {
            if (Host != null)
                await Host.StopAsync(CancellationToken.None);

            DeleteTempDataDirectory();
        }

        // Accessors

        public IHost? Host { get; set; } = null;

        public IServiceProvider? Services => Host?.Services;

        #endregion

        #region IDisposable
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Host?.Dispose();
                    DeleteTempDataDirectory();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
