using System.Net;
using System.Text.Json.Serialization;

namespace OpenCMS.Shared.Models.InputModels
{
    partial interface IBaseResponse
    {

    }
    public class BaseResponse<T>:IBaseResponse
    {
        public T Data { get; set; }
        [JsonPropertyName("status_code")]
        public HttpStatusCode HttpStatusCode { get; set; }
    }
    public class PaginatedResponse<T>
    {
        [JsonPropertyName("data")]
        public PaginateItems<T> Data { get; set; }
        [JsonPropertyName("status_code")]
        public HttpStatusCode HttpStatusCode { get; set; }
    }

    public class PaginateItems<T>
    {
        [JsonPropertyName("items")]
        public T Items { get; set; }
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }

    public class ErrorResponse: IBaseResponse
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

        [JsonPropertyName("status_code")]
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
