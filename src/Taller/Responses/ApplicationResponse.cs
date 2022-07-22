using System.Net;

namespace RCVUcabBackend.Responses
{
    public class ApplicationResponse<T> where T: class
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public int DataInsert { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Exception { get; set; }
    }
}