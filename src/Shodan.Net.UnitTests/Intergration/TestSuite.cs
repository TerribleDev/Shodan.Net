using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Shodan.Net.UnitTests.Intergration
{
    public class TestSuite
    {
        private readonly ShodanClient shodanClient = new ShodanClient("9F0mxmNSaHbe0mYmefwoCZrChT2h0KzC");

        [Fact]
        public async Task GetHost()
        {
            var result = await shodanClient.GetHostAsync("41.21.249.170");
        }

        [Fact]
        public async Task PerformSearch()
        {
            var result = await shodanClient.SearchHosts(a => a.With_state("NY"));
        }
    }
}