using System.Collections.Generic;

namespace repopractise.Helpers
{
    public class ApiResponseList<T>
    {

        public ApiResponseCodes Status { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
    }
}