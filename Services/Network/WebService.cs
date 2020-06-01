using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using repopractise.Domain.Dtos.ApiClientResponse;
using repopractise.Domain.Models;
using repopractise.Helpers;
using repopractise.Network.Api;

namespace repopractise.Services.Network
{
    public class WebService : IWebService
    {
        private readonly IApiClient _apiClient;
        private readonly Dictionary<string, string> headerMap;
        private readonly string baseUrl;

        private readonly ILogger _logger;
        
        public WebService(IApiClient apiClient, IConfiguration configuration, ILogger<WebService> logger) 
        {
            _apiClient = apiClient;
            _logger = logger;
            baseUrl = configuration["OdataApi:BaseUrl"];

            headerMap = new Dictionary<string, string>();
            headerMap.Add("Content-Type", "application/json");

        }

        public async Task<ApiResponse<List<ODataModel>>> GetOdataQueryData(string field) 
        {

            ApiResponse<List<ODataModel>> response = new ApiResponse<List<ODataModel>>();

            try
            {

                var endpoint = field == null ? "/odata/students?" : "/odata/students?$select=" + field + "&";
                var url = baseUrl + endpoint + "$format=json";


                var data = await _apiClient.GetDataAsync(url, headerMap);

                var jsondata = (OdataResponse<ODataModel>)JsonConvert.DeserializeObject<OdataResponse<ODataModel>>(data);

                response.Message = "Data retrieved";
                response.Status = ApiResponseCodes.Success;
                response.Data = jsondata.Value;

                return response;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.StackTrace);
                _logger.Log(LogLevel.Error, ex.Message);

                response.Message = "An error occured while updating user bio";
                response.Data = null;
                response.Status = ApiResponseCodes.BadRequest;

                return response;
            }



        }


    }
}