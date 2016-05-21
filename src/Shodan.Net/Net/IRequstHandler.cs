using Shodan.Net.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shodan.Net
{
    /// <summary>
    /// sane wrapper of http, and simple abstraction layer for unit testing
    /// </summary>
    public interface IRequstHandler : IDisposable
    {
        Task<T> MakeRequestAsync<T>(Uri url, HttpContent content = null, RequestType requstType = RequestType.GET)
            where T : class;
    }
}