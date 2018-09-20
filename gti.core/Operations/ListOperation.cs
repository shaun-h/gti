using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using gti.core.Interfaces;
using gti.core.Models;
using gti.core.Models.List;
using Newtonsoft.Json;

namespace gti.core.Operations
{
    public class ListOperation : IOperation
    {
        private ListOperationOptions _options;
        
      
        public void SetOperationOptions(IOperationOptions options)
        {
            var op = options as ListOperationOptions;
            if (op == null)
            {
                throw new ArgumentException(nameof(options));
            }
            else
            {
                _options = op;
            }
        }

        public void PerformOperation()
        {
            
        }

        private async Task<T> GetResponseFromJson<T>(string uri)
        {
            var client = new HttpClient();
//            client.BaseAddress = new Uri(uri);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                throw new HttpRequestException($"Request to {response.RequestMessage.RequestUri} failed, with status code {response.StatusCode}");
            }
        }
    }
}