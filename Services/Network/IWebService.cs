using System.Threading.Tasks;
using repopractise.Domain.Dtos.ApiClientResponse;
using repopractise.Helpers;
using repopractise.Domain.Models;
using System.Collections.Generic;

namespace repopractise.Services.Network
{
    public interface IWebService
    {
        Task<ApiResponse<List<ODataModel>>> GetOdataQueryData(string field);
    }
}