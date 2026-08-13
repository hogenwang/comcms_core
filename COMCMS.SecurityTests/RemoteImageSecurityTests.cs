using System.Net;
using COMCMS.Web.Services;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class RemoteImageSecurityTests
    {
        [Theory]
        [InlineData("127.0.0.1")]
        [InlineData("10.1.2.3")]
        [InlineData("172.16.0.1")]
        [InlineData("192.168.1.1")]
        [InlineData("169.254.169.254")]
        [InlineData("::1")]
        [InlineData("fc00::1")]
        [InlineData("fe80::1")]
        public void PrivateAndLocalAddresses_AreRejected(string value)
        {
            Assert.False(RemoteImageService.IsPublicAddress(IPAddress.Parse(value)));
        }

        [Theory]
        [InlineData("1.1.1.1")]
        [InlineData("8.8.8.8")]
        [InlineData("2606:4700:4700::1111")]
        public void PublicAddresses_AreAllowed(string value)
        {
            Assert.True(RemoteImageService.IsPublicAddress(IPAddress.Parse(value)));
        }
    }
}
