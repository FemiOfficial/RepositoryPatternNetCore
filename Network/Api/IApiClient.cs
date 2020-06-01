using System.Collections.Generic;
using System.Threading.Tasks;

namespace repopractise.Network.Api
{
    public interface IApiClient
    {
        public Task<string> PostDataAsync<T>(string endPoint, Dictionary<string, string> headers, T dto);

        public Task<string> GetDataAsync(string endPoint, Dictionary<string, string> headers);
    }
}