using System.Net;

namespace Auto_Invitation
{
    public class HttpResponseModel<T>
    {
        public HttpStatusCode Status { get; set; }
        public T Data { get; set; }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}