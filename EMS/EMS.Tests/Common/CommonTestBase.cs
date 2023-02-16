using Microsoft.Extensions.Configuration;

namespace EMS.Tests.Common
{
    public abstract class CommonTestBase<TSut> : IDisposable
        where TSut : CommonSutBase, new()
    {
        private bool disposedValue;

        /// <summary>
        /// Standard constructor
        /// </summary>
        /// <param name="sut">SUT to use for this test</param>
        protected CommonTestBase()
        {
            SUT = new TSut();
        }

        /// <summary>
        /// System under test
        /// </summary>
        protected TSut? SUT { get; private set; }

        /// <summary>
        /// SUT Services available to use in tests
        /// </summary>
        protected IServiceProvider? Services => SUT?.Services;

        /// <summary>
        /// Start the SUT for this test
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        /// <exception cref="TestException"></exception>
        protected Task StartSutAsync(IConfiguration? configuration = null)
        {
            if (SUT == null)
            {
                throw new Exception("SUT cannot be null");
            }
            return SUT.StartSUTAsync(configuration);
        }

        /// <summary>
        /// Stop the SUT running for this test
        /// </summary>
        /// <returns>Task</returns>
        protected async Task StopSUTAsync()
        {
            if (SUT == null)
                return;
            await SUT.StopSUTAsync();
            SUT = null;
        }

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    StopSUTAsync().Wait();
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
