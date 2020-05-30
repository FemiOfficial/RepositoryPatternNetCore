namespace repopractise.Helpers
{
    public class ApiResponse<T>
    {
        public ApiResponseCodes Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}