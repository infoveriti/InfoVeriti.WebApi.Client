using InfoVeriti.Libs.Network.Abstracts.WebClient;

namespace InfoVeriti.Libs.Network.WebClient
{
    public class TransparentInterceptor : IWebClientInterceptor
    {

        public HttpResponseMessage OnIntercept( WebClientOnInterceptParameters parameters )
        {
            return parameters.Response;
        }
    }
}