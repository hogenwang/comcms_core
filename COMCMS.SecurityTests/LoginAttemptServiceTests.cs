using System.Threading.Tasks;
using COMCMS.Web.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class LoginAttemptServiceTests
    {
        [Fact]
        public async Task AccountLimit_AggregatesAcrossIpAddresses()
        {
            var service = CreateService();
            for (var index = 0; index < 10; index++)
                await service.RecordFailureAsync(" ExampleUser ", $"192.0.2.{index}");

            Assert.True(await service.IsBlockedAsync("exampleuser", "198.51.100.1"));
        }

        [Fact]
        public async Task IpLimit_AggregatesAcrossAccounts()
        {
            var service = CreateService();
            for (var index = 0; index < 10; index++)
                await service.RecordFailureAsync($"user-{index}", "192.0.2.10");

            Assert.True(await service.IsBlockedAsync("new-user", "192.0.2.10"));
        }

        [Fact]
        public async Task Success_ClearsAccountFailures()
        {
            var service = CreateService();
            for (var index = 0; index < 10; index++)
                await service.RecordFailureAsync("exampleuser", $"192.0.2.{index}");

            await service.RecordSuccessAsync("EXAMPLEUSER");

            Assert.False(await service.IsBlockedAsync("exampleuser", "198.51.100.1"));
        }

        private static LoginAttemptService CreateService() =>
            new(new ServiceCollection().BuildServiceProvider());
    }
}
