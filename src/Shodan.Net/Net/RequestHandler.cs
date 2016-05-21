using Newtonsoft.Json;
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
    public class RequestHandler : IRequstHandler
    {
        private HttpClient client { get; set; }

        public async Task<T> MakeRequestAsync<T>(Uri url, HttpContent content = null, RequestType requstType = RequestType.GET)
           where T : class
        {
            if(requstType != RequestType.GET && content == null)
            {
                throw new ShodanException($"Request type {requstType} requires content");
            }
            if(requstType == RequestType.DELETE || requstType == RequestType.PUT)
            {
                throw new NotImplementedException("Put and Delete requests have not been implemented properly");
            }
            HttpResponseMessage connection = null;
            if(requstType == RequestType.GET)
            {
                connection = await client.GetAsync(url);
            }
            else if(requstType == RequestType.POST)
            {
                connection = await client.PostAsync(url, content);
            }

            //todo error handle based on user input
            connection.EnsureSuccessStatusCode();

            //var statusCode = (int)connection.StatusCode;
            //if(statusCode != 200 && statusCode != 201 && statusCode == 202)
            //{
            //    System.Diagnostics.Trace.TraceError("Error calling shodan API, Status code is not 200" + JsonConvert.SerializeObject(connection.s))
            //    //todo error handle
            //    return null;
            //}
            var readResult = await connection.Content.ReadAsStringAsync();
            if(typeof(T) == typeof(string))
            {
                return readResult as T;
            }
            return JsonConvert.DeserializeObject<T>(readResult);
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    client.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}