using System.Collections.Generic;

namespace repopractise.Domain.Dtos.ApiClientResponse
{
    public class OdataResponse<T>
    {
        public List<T> Value { get; set; }
    }
}