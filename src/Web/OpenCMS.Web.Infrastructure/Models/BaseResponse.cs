using System.Net;
using System.Text.Json.Serialization;

namespace OpenCMS.Web.Infrastructure.Models
{
    public class BaseResponse
    {
        [JsonPropertyName("status_code")]
        public HttpStatusCode HttpStatusCode { get; set; }
    }
    public class BaseResponse<T>: BaseResponse
    {
        
        [JsonPropertyName("data")]
        public T Data { get; set; }
       
    }
    public class PaginatedBaseResponse<T>
    {
        [JsonPropertyName("data")]
        public PaginatedBaseItems<T> Data { get; set; }
        [JsonPropertyName("status_code")]
        public HttpStatusCode HttpStatusCode { get; set; }
    }
    public class PaginatedBaseItems<T>
    {
        [JsonPropertyName("items")]
        public T Items { get; set; }
       
    }
    public class ErrorResponse :BaseResponse
    {
        public ErrorResponse()
        {
            this.HttpStatusCode = HttpStatusCode.BadRequest;
        }

        public ErrorResponse(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
            this.HttpStatusCode = HttpStatusCode.BadRequest;
        }
        [JsonPropertyName("error_message")]
        public string ErrorMessage { get; set; }

       
    }
}
