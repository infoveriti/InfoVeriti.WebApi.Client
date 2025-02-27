using InfoVeriti.Libs.Network.Abstracts.HttpClient;

namespace InfoVeriti.Libs.Network.Abstracts.WebClient
{
    public class WebClientOnInterceptParameters
    {
        public static WebClientOnInterceptParameters CreateInstance( IHttpClient httpClient, HttpRequestMessage request, HttpResponseMessage response )
        {
            return new WebClientOnInterceptParameters( httpClient, request, response );
        }

        public IHttpClient HttpClient { get; }
        
        public HttpRequestMessage Request  { get; }

        public HttpResponseMessage Response { get; }

        private WebClientOnInterceptParameters( IHttpClient httpClient, HttpRequestMessage request, HttpResponseMessage response )
        {
            HttpClient = httpClient ?? throw new ArgumentNullException( nameof( httpClient ) );
            Request = request ?? throw new ArgumentNullException( nameof( request ) );
            Response = response ?? throw new ArgumentNullException( nameof( response ) );
        }
    }
}