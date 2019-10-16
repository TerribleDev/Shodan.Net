using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Shodan.Net.UnitTests.Integration
{
    [TestClass]
    public class TestSuite
    {
        private readonly ShodanClient shodanClient = new ShodanClient("9F0mxmNSaHbe0mYmefwoCZrChT2h0KzC");

        [TestMethod]
        public async Task GetHost()
        {
            var result = await shodanClient.GetHostAsync("41.21.249.170");
        }

        [TestMethod]
        public async Task PerformSearch()
        {
            var result = await shodanClient.SearchHosts(a => a.With_state("NY"));
        }
    }
}