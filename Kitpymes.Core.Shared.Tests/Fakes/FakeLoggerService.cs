using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Kitpymes.Core.Shared.Tests
{
    public interface IFakeLoggerService
    {
        int AddTwoPositiveNumbers(int a, int b);
    }

    public class FakeLoggerService : IFakeLoggerService
    {
        private readonly ILogger<FakeLoggerService> _logger;

        public FakeLoggerService()
        {
            _logger = NullLogger<FakeLoggerService>.Instance;
        }

        public int AddTwoPositiveNumbers(int a, int b)
        {
            if (a <= 0 || b <= 0)
            {
                _logger.LogError("Arguments should be both positive.");
                return 0;
            }
            _logger.LogInformation($"Adding {a} and {b}");
            return a + b;
        }
    }
}
