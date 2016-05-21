using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Shodan.Net.UnitTests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Class1
    {
        [Fact]
        public async Task privateGetsPorts()

        {
            var client = new ShodanClient("9F0mxmNSaHbe0mYmefwoCZrChT2h0KzC");
            var ports = await client.GetPortsAsync();
        }
    }
}