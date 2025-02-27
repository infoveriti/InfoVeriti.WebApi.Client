using System.Net;

namespace InfoVeriti.Libs.Network.Exceptions
{
    public class HttpStatusCodeException: Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public HttpRequestMessage Request { get; private set; }
        
        public object? BodyData { get; private set; }
        
        public string? BodyText { get; private set; }

        public HttpStatusCodeException( HttpStatusCode statusCode, HttpRequestMessage requestMessage, object? bodyData, string? bodyText ) : base( GenerateMessage( statusCode, requestMessage ) )
        {
            StatusCode = statusCode;
            Request = requestMessage;
            BodyData = bodyData;
            BodyText = bodyText;
        }
        
        public HttpStatusCodeException( HttpStatusCode statusCode, HttpRequestMessage requestMessage, object? bodyData, string? bodyText, Exception requestException ) : base( GenerateMessage( statusCode, requestMessage ), requestException )
        {
            StatusCode = statusCode;
            Request = requestMessage;
            BodyData = bodyData;
            BodyText = bodyText;
        }


        private static string GenerateMessage( HttpStatusCode statusCode, HttpRequestMessage requestMessage )
        {
            return $"Receive Wrong HTTP Status Code [{(int) statusCode}: {statusCode}] for url: {requestMessage.Method} {requestMessage.RequestUri}";
        }

    }
}