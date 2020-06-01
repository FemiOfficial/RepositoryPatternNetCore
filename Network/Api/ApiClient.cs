

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace repopractise.Network.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiClient> _logger;

        public ApiClient(HttpClient httpClient, ILogger<ApiClient> logger) 
        {
            _httpClient = httpClient;
            _logger = logger;
        }


        public async Task<string> PostDataAsync<T>(string endPoint, Dictionary<string, string> headers, T dto)
        {
            foreach (var pair in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
            }
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            _logger.Log(LogLevel.Information, "Request Body Content: " + content.ReadAsStringAsync().Result);

            var httpResponse = await _httpClient.PostAsync(endPoint, content);

            return await parseHttpResponse(httpResponse);
        }

        public async Task<string> GetDataAsync(string endPoint, Dictionary<string, string> headers)
        {
            foreach (var pair in headers)
            {
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(pair.Key, pair.Value);
            }

            var httpResponse = await _httpClient.GetAsync(endPoint);
            return await parseHttpResponse(httpResponse);

        }

        private async Task<string> parseHttpResponse(HttpResponseMessage httpResponse)
        {
            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorContent = await httpResponse.Content.ReadAsStringAsync();
                var message = $"*[{(int)httpResponse.StatusCode}] error occured at external api: {errorContent}";
                _logger.Log(LogLevel.Information, $"Error message : {message}");

                throw new Exception(message);
            }

            var jsonString = await httpResponse.Content.ReadAsStringAsync();
            _logger.Log(LogLevel.Information, $"Response body content : {jsonString}");
            return jsonString;
        }



    }
}