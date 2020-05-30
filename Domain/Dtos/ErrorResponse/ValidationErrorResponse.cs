
using System.Collections.Generic;

namespace repopractise.Domain.Dtos.ErrorResponse
{
    public class ValidationErrorResponse
    {
        public List<ErrorDto> Errors { get; set; } = new List<ErrorDto>();
    }
}