namespace InfoVeriti.Libs.Network.Abstracts.WebClient
{
    public interface IWebClientInterceptor
    {
        HttpResponseMessage OnIntercept( WebClientOnInterceptParameters  parameters );
    }
}